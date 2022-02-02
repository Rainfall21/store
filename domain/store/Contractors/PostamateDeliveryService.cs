using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Contractors
{
    public class PostamateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
        {
            {"1", "Moscow" },
            {"2", "Saint-Petersburg" },
        };
        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1",
                new Dictionary<string, string>
                {
                    {"1", "Krasnogorskiy boulevard" },
                    {"2", "Lenina street" },
                    {"3", "Zheleznodorozhnaya street" },
                }
            },
            {
                "2",
                new Dictionary<string, string>
                {
                    {"4", "Moscow railway station" },
                    {"5", "Gostiny dvor" },
                    {"6", "Nevsky Prospekt" },
                }
            }
        };
        public string Name => "Postamate";

        public string Title => "Delivery via postamates in Moscow and Saint-Petersburg";

        public Form FirstForm(Order order)
        {
            return Form.CreateFirst(Name)
                .AddParameter("orderId", order.Id.ToString())
                .AddField(new SelectionField("City", "city", "1", cities));
        }
        public Form NextForm(int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                {
                    return Form.CreateNext(Name, 2, values)
                        .AddField(new SelectionField("Postamate", "postamate", "1", postamates["1"]));
                }
                else if (values["city"] == "2")
                {
                    return Form.CreateNext(Name, 2, values)
                        .AddField(new SelectionField("Postamate", "postamate", "4", postamates["2"]));
                }
                else
                    throw new InvalidOperationException("Invalid postamate city");
            }
            else if (step == 2)
            {
                return Form.CreateLast(Name, 3, values);
            }
            else
                throw new InvalidOperationException("Invalid postamate step");
        }
        public OrderDelivery GetDelivery(Form form)
        {
            if (form.ServiceName != Name || !form.IsFinal)
                throw new InvalidOperationException("Invalid form");

            var cityId = form.Parameters["city"];
            var cityName = cities[cityId];
            var postamateId = form.Parameters["postamate"];
            var postamateName = postamates[cityId][postamateId];

            var parameters = new Dictionary<string, string>
            {
                { nameof(cityId), cityId },
                { nameof(cityName), cityName },
                { nameof(postamateId), postamateId },
                { nameof(postamateName), postamateName },
            };
            var description = $"City:{cityName}\nPostamate:{postamateName}";

            return new OrderDelivery(Name, description, 150m, parameters);
        }
    }
}
