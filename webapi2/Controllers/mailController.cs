﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi2;
using System.Net.Http.Handlers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace webapi2.Controllers
{
    [RoutePrefix("api/mail")]
    public class mailController : ApiController
    {

        static private Notification_SystemEntities3 db = new Notification_SystemEntities3();
        static int smsCount = db.mailQueues.Count();
        [HttpPost]
        public string mailcreate(int id, [FromBody] dynamic value)
        {
            String[] variables;
            string tmp = "";
            string to;
            dynamic sValue1 = value;
            try
            {
                var re = new HttpResponseMessage(HttpStatusCode.OK);
                string con = db.Notification_tb.Single(p => p.id == id).content;
                string []variables2 = sValue1.Split(':');
                to = variables2[0];
                variables = variables2[1].Split(',');
                string[] separatingStrings = { "...." };
                string[] Original = con.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                if (Original.Length == variables.Length)
                {
                    int j = 0;

                    for (int i = 0; i < con.Length;)
                    {
                        if (con[i] == '.' && con[i + 1] == '.' && con[i + 2] == '.' && con[i + 3] == '.')
                        {
                            tmp += variables[j];
                            i += 4;
                            j++;

                        }
                        else
                        {
                            tmp += con[i];
                            i++;
                        }
                    }
                }
                mailQueue sms = new mailQueue();
                sms.id = 15;
                sms.content = tmp;
                sms.toUser = to;
                db.mailQueues.Add(sms);
                var res = new HttpResponseMessage(HttpStatusCode.OK);
                db.SaveChanges();
                return tmp;
            }
            catch
            {
                return "Error";
            }
        }

    }
}
