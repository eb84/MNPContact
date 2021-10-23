using Microsoft.AspNetCore.Mvc;
using MNPContactAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace MNPContactAPI.Controllers
{
    public class ContactsController : ApiController
    {
        internal static ContactModel __contactModel = new ContactModel();

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Contact contact = __contactModel.Contacts.Find(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult List()
        {
            return Ok(__contactModel.Contacts.ToList());
        }

        [HttpPost]
        [HttpPatch]
        public IHttpActionResult Post([FromBody] Contact contact, int id = -1)
        {
            if (id > 0)
            {
                if (contact.id <= 0)
                {
                    contact.id = id;
                }
                else if (id != contact.id)
                {
                    return BadRequest("id specified in URL does not match id specified in content. id field is read-only.");
                }
            }

            try
            {
                //validate first
                ValidationContext context = new ValidationContext(contact, serviceProvider: null, items: null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(contact, context, validationResults, true))
                {
                    string validationMessage = string.Empty;
                    foreach (ValidationResult result in validationResults)
                    {
                        validationMessage += result.ErrorMessage + " ";
                    }

                    validationMessage = $"Validation failed for the following reasons: [{validationMessage.Trim()}]";
                    return BadRequest(validationMessage);
                }

                if (contact.id > 0)
                {
                    Contact existing = __contactModel.Contacts.Find(contact.id);
                    if (existing == null)
                    {
                        return NotFound();
                    }

                    existing.Name = contact.Name;
                    existing.JobTitle = contact.JobTitle;
                    existing.Company = contact.Company;
                    existing.Address = contact.Address;
                    existing.Phone = contact.Phone;
                    existing.Email = contact.Email;
                    existing.LastContacted = contact.LastContacted;
                    existing.Comments = contact.Comments;

                    if (!__contactModel.ChangeTracker.HasChanges() || __contactModel.SaveChanges() > 0)
                    {
                        return Ok(contact);
                    }
                    else
                    {
                        throw new Exception("Failed to update existing Contact.");
                    }
                }
                else
                {
                    __contactModel.Contacts.Add(contact);
                    if (__contactModel.SaveChanges() > 0)
                    {
                        return Created(contact.id.ToString(), contact);
                    }
                    else
                    {
                        throw new Exception("Failed to create new Contact.");
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult Status()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Ok($"MNPContactAPI {version} is ready");
        }
    }
}
