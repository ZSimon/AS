using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Core.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using Nop.Plugin.Misc.ProductEditor.Models;
using Nop.Core.Data;
using System.Net;
using System.Web;
using Nop.Plugin.Misc.ProductEditor.Filters;

namespace Nop.Plugin.Misc.ProductEditor.Controllers
{
    namespace Nop.Plugin.Misc.ProductEditor.Controllers
    {
        public class UserProfileController : BaseController
        {
            [HttpGet]
            public string test()
            {

                return "test";
            }
            /*
            [OutputCache(Duration=500)]
            public JsonResult QA_translate()
            {

               return Json(new {time=TimeSpan. });
            }
            */
            //http://www.alpstories.si/en/UserProfile/QA
            [HttpGet, CompressFilter]
            public JsonResult Qa()
            {

                List<QAModel> questions = new List<QAModel>();
                try
                {
                    //var customer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;
                    Int32 languageId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;

                    var cs = new DataSettingsManager().LoadSettings().DataConnectionString;
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.P_QAGet", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@LanguageId", SqlDbType.Int).Value = languageId;

                            conn.Open();

                            var rdr = cmd.ExecuteReader();
                            Int32 id = 0;
                            QAModel question=null;
                            while (rdr.Read())
                            {
                                if (rdr.GetInt32(0) != id)
                                {
                                    if (question != null)
                                    {
                                        questions.Add(question);
                                    }
                                    id = rdr.GetInt32(0);
                                    question = new QAModel
                                    {
                                        Id = id,
                                        ControlTypeId = rdr.GetInt32(1),
                                        Question = rdr.GetString(2)
                                    };
                                }

                                Answer answer = new Answer
                                {
                                    Id = rdr.GetInt32(3),
                                    Name = rdr.GetString(4),
                                    IsPreSelected = rdr.GetBoolean(5)
                                };
                                question?.Answers.Add(answer);
                            }
                            rdr.Close();

                            if (question != null)
                                questions.Add(question);

                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message, qa = "" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, msg="", qa = questions } , JsonRequestBehavior.AllowGet);
            }

            /*? pomeni, da je lahko null*/
           public JsonResult UpdateProfile(String name, Int32? profileId, Boolean? delete)
            {
                try
                {
                    var cs = new DataSettingsManager().LoadSettings().DataConnectionString;
                    
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.P_UpdateProfile", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("CustomerId", SqlDbType.Int);
                            cmd.Parameters.Add("ProfileId", SqlDbType.Int);
                            if (profileId == null)
                            {
                                var customer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                                cmd.Parameters["CustomerId"].Value = customer.Id;
                            }
                            else
                                cmd.Parameters["ProfileId"].Value = profileId;

                            cmd.Parameters.Add("ProfileName", SqlDbType.NVarChar, 100);
                            if(name!= null)
                                cmd.Parameters["ProfileName"].Value = name;

                            cmd.Parameters.Add("Delete", SqlDbType.Bit);
                            if (delete == true)
                                cmd.Parameters["Delete"].Value = 1;

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message, profiles = "" }, JsonRequestBehavior.AllowGet);
                }
                
                return Json(new { success = true, msg = name}, JsonRequestBehavior.AllowGet);
            }

            /*det question and answers of some profile*/
           public JsonResult QaProfile() {
                List<QAProfile> profiles = new List<QAProfile>();
                try
                {
                    var customer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                    var cs = new DataSettingsManager().LoadSettings().DataConnectionString;
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.P_QAGetProfile", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customer.Id; ;

                            conn.Open();
                            var rdr = cmd.ExecuteReader();

                            Int32 id = 0;
                            QAProfile profile = null;
                            while (rdr.Read())
                            {
                                if (rdr.GetInt32(0) != id)
                                {
                                    if (profile != null)
                                    {
                                        profiles.Add(profile);
                                    }
                                    id = rdr.GetInt32(0);
                                    profile = new QAProfile
                                    {
                                        ProfileId = id,
                                        Name = rdr.GetString(1)
                                    };
                                }


                                if (rdr.IsDBNull(2)) continue;/*jump to next iteration of while*/
                                ProfileAnswer profileAnswer = new ProfileAnswer
                                {
                                    QuestionId = rdr.GetInt32(2),
                                    AnswerId = rdr.GetInt32(3)
                                };

                                profile?.ProfileAnswers.Add(profileAnswer);
                            }
                            rdr.Close();

                            if (profile != null)
                                profiles.Add(profile);

                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message, profiles = "" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, msg = "", profiles = profiles }, JsonRequestBehavior.AllowGet);
                /*
                var requestedETag = Request.Headers["If-None-Match"];
                var responseETag = "1e"; // lookup or generate etag however you want
                if (requestedETag == responseETag)
                    return new HttpStatusCodeResult(HttpStatusCode.NotModified);

                Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
                Response.Cache.SetETag(responseETag);

                //if (Request.Headers["If-Modified-Since"] != null && Count % 2 == 0)
                //{
                //    return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
                //}

                //Response.Cache.SetExpires(DateTime.Now.AddSeconds(5));
                //Response.Cache.SetLastModified(DateTime.Now);
                */
            }

            [HttpPost]
            public JsonResult SaveProfile(QAProfile profile)
            {
                try
                {
                    var cs = new DataSettingsManager().LoadSettings().DataConnectionString;
                    var profileAnswers = profile.ProfileAnswers;
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.P_QAUpdateProfile", conn))
                        {
                            var dt = new DataTable();
                            dt.Columns.Add("QuestionId", typeof(Int32));
                            dt.Columns.Add("AnswerId", typeof(Int32));

                            foreach (ProfileAnswer t in profileAnswers)
                            {
                                dt.Rows.Add(t.QuestionId, t.AnswerId);
                            }

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value = profile.ProfileId;
                            cmd.Parameters.Add(new SqlParameter("qa", SqlDbType.Structured));
                            cmd.Parameters["qa"].Value = dt;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message, profiles = "+" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.DenyGet);
            }
        }
    }
}
