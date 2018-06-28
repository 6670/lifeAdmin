

using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace WebSeearchMediaService.Model
{
    public class Repository : IRepository
    {
        //public int LeadCapture(object lead, string url)
        //{
        //    try
        //    {
        //        string jsonObj = JsonConvert.SerializeObject(lead);

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(jsonObj);
        //            streamWriter.Flush();
        //            streamWriter.Close();
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();
        //            var jsonResult = JsonConvert.DeserializeObject<dynamic>(result);
        //            string status = jsonResult?.result;
        //            if (status == "success")
        //            {
        //                int leadId = Convert.ToInt32(jsonResult.ref_id);
        //                return leadId;
        //            }
        //        }

        //        return 0;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}       

        public int LeadSend(string url)
        {
            try
            {
                int leadId = 0;
                var client = new HttpClient();
                var task = client.GetAsync(url)
                  .ContinueWith((taskwithresponse) =>
                  {
                      var response = taskwithresponse.Result;
                      var jsonString = response.Content.ReadAsStringAsync();
                      jsonString.Wait();
                      var jsonResult = JsonConvert.DeserializeObject<dynamic>(jsonString.Result);
                      string status = jsonResult?.result;
                      if (status == "success")
                      {
                          leadId = Convert.ToInt32(jsonResult.ref_id);
                      }
                  });
                task.Wait();

                return leadId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}