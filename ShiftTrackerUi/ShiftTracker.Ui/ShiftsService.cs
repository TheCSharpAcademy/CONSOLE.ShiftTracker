using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ShiftTracker.Ui
{
    public class ShiftsService
    {
        private RestClient client = new("https://localhost:7116/api/");

        public void GetShifts()
        {

            var request = new RestRequest("shifts");
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;

                var serialize = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

                TableVisualisationEngine.ShowTable(serialize, "Categories Menu");

            }
        }

        public IRestResponse<Shift> GetShiftById(int id)
        {

            var request = new RestRequest($"shifts/{id}");
            var response = client.Execute<Shift>(request);
            return response;
        }

        public void AddShift(Shift shift)
        {
            var request = new RestRequest("shifts", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(shift));
            var response = client.Execute<Shift>(request);
            Console.WriteLine(response.Content);
        }

        public IRestResponse<Shift> DeleteShift(int id)
        {
            var request = new RestRequest($"shifts/{id}", Method.DELETE);
            var response = client.Execute<Shift>(request);
            Console.WriteLine(response.Content);
            return response;
        }

        public void UpdateShift(Shift shift)
        {
            var request = new RestRequest($"shifts/{shift.ShiftId}", Method.PUT);
            request.AddJsonBody(JsonConvert.SerializeObject(shift));
            var response = client.Execute<Shift>(request);
            Console.WriteLine(response.Content);
        }
    }
}
