using MobileServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MobileServer.Controllers
{
   
    public class ValuesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                var context = new ApplicationDbContext();
                IEnumerable<Entry> entries = context.entries.ToList();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(entries));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            } catch(Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // GET api/values/5
        public HttpResponseMessage Get(string id)
        {
            try { 
                var context = new ApplicationDbContext();
                var result = context.entries.ToList().Find(e => e.id == id);
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(result));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            } catch(Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
    }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Entry entry)
        {
            try
            {
                var context = new ApplicationDbContext();
                entry.id = Guid.NewGuid().ToString();
                context.entries.Add(entry);
                context.SaveChanges();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(entry));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("api/values/dto")]
        public HttpResponseMessage Post([FromBody]EntriesListDTO entries)
        {
            try
            {
                var context = new ApplicationDbContext();
                foreach (Entry entry in entries.entriesList)
                {
                    
                    entry.id = Guid.NewGuid().ToString();
                    context.entries.Add(entry);
                    context.SaveChanges();
                }
                var responseList = context.entries.ToList();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(entries));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(string id, [FromBody]Entry value)
        {
            try
            {
                var context = new ApplicationDbContext();
                //var entryToDelete = context.entries.SingleOrDefault(e => e.id == id);
                //entryToDelete = value;
                //context.SaveChanges();
                var entryToUpdate = context.entries.Find(id);
                //entryToUpdate = value;
                //context.SaveChanges();
                context.Entry(entryToUpdate).CurrentValues.SetValues(value);
                context.SaveChanges();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(entryToUpdate));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                var context = new ApplicationDbContext();
                var entryToDelete = context.entries.ToList().Find(e => e.id == id);
                var result = context.entries.Remove(entryToDelete);
                context.SaveChanges();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(result));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
