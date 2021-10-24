using Microsoft.VisualStudio.TestTools.UnitTesting;
using MNPContactAPI.Controllers;
using MNPContactAPI.Models;
using Microsoft.CSharp.RuntimeBinder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Results;

namespace MNPContactAPI.Tests
{
    [TestClass]
    public class UnitTest
    {
        private ContactsController _contactsController = new ContactsController();
        private Contact _defaultContact = new Contact()
        {
            Name = "Tester",
            JobTitle = "Tester",
            Company = "HP",
            Address = "Address",
            Phone = "Phone",
            Email = "test@email.com"
        };

        [TestMethod]
        public void MandatoryFieldsEnforced()
        {
            Contact test = new Contact();

            dynamic result = _contactsController.Post(test);
            Assert.IsTrue(result is BadRequestErrorMessageResult);

            Assert.IsTrue(result.Message.ToLower().Contains("name"));
            Assert.IsTrue(result.Message.ToLower().Contains("jobtitle"));
            Assert.IsTrue(result.Message.ToLower().Contains("company"));
            Assert.IsTrue(result.Message.ToLower().Contains("address"));
            Assert.IsTrue(result.Message.ToLower().Contains("phone"));
            Assert.IsTrue(result.Message.ToLower().Contains("email"));

            result = _contactsController.Post(_defaultContact);
            Assert.IsTrue(result is CreatedNegotiatedContentResult<Contact>);
        }

        [TestMethod]
        public void EmailFormatEnforced()
        {
            _defaultContact.Email = "invalid";
            var result = _contactsController.Post(_defaultContact);
            Assert.IsTrue(result is BadRequestErrorMessageResult);
            Assert.IsTrue(((BadRequestErrorMessageResult)result).Message.ToLower().Contains("email"));
        }

        [TestMethod]
        public void NoPhoneFormatEnforcement() //https://github.com/google/libphonenumber/blob/master/FALSEHOODS.md
        {
            Contact before = (Contact)_defaultContact.Clone();

            before.Phone = "DO NOT CALL";
            PerformPhoneAssertion(ref before, out Contact after);

            before.Phone = "19055551234";
            PerformPhoneAssertion(ref before, out after);

            before.Phone = "+1(905)555-1234";
            PerformPhoneAssertion(ref before, out after);

            before.Phone = "+1 (905) 555 1234";
            PerformPhoneAssertion(ref before, out after);

            before.Phone = "19055551234 x4321";
            PerformPhoneAssertion(ref before, out after);

            before.Phone = "1-800-MICROSOFT";
            PerformPhoneAssertion(ref before, out after);

            before.Phone = "+54 9 2982 123456";
            PerformPhoneAssertion(ref before, out after);

            before.Phone = "02982 15 123456";
            PerformPhoneAssertion(ref before, out after);

            //etc, etc
        }

        private void PerformPhoneAssertion(ref Contact before, out Contact after)
        {
            dynamic result = _contactsController.Post(before);
            Assert.IsTrue(result is CreatedNegotiatedContentResult<Contact> || result is OkNegotiatedContentResult<Contact>);
            after = result.Content;
            Assert.AreEqual(before, after);
            before = after;
        }

        [TestMethod]
        public void CompanyEnforced()
        {
            _defaultContact.Company = "No Way This Is A Real Company";
            var result = _contactsController.Post(_defaultContact);
            Assert.IsTrue(result is BadRequestErrorMessageResult);
            Assert.IsTrue(((BadRequestErrorMessageResult)result).Message.ToLower().Contains("company"));
        }

        [TestMethod]
        public void PostingResultContainsContact()
        {
            var result = _contactsController.Post(_defaultContact);
            Assert.IsTrue(result is CreatedNegotiatedContentResult<Contact>);
            Assert.AreEqual(_defaultContact, ((CreatedNegotiatedContentResult<Contact>)result).Content);
        }

        [TestMethod]
        public void CreatingContactReturnsNewID()
        {
            var result = _contactsController.Post(_defaultContact);
            Assert.IsTrue(result is CreatedNegotiatedContentResult<Contact>);
            Assert.IsTrue(((CreatedNegotiatedContentResult<Contact>)result).Content.id > 0);
        }

        [TestMethod]
        public void UpdatingContactReturnsChanges()
        {
            Contact before = _defaultContact;

            dynamic result = _contactsController.Post(before);
            Assert.IsTrue(result is CreatedNegotiatedContentResult<Contact>);
            Contact after = result.Content;
            Assert.AreEqual(before, after);

            before = after;
            Contact originalBefore = (Contact)before.Clone();
            before.JobTitle = "Different title";

            result = _contactsController.Post(before);
            Assert.IsTrue(result is OkNegotiatedContentResult<Contact>);
            after = result.Content;
            Assert.AreNotEqual(originalBefore, after);
        }
    }
}
