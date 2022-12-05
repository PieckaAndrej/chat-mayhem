using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace WebApp.Models
{
    public class QuestionContractResolver<T> : DefaultContractResolver
    {

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.UnderlyingName == nameof(Question<T>.Answers))
            {
                foreach (var attribute in System.Attribute.GetCustomAttributes(typeof(T)))
                {
                    if (attribute is JsonObjectAttribute jobject)
                    {
                        property.PropertyName = jobject.Title;
                    }
                }
            }
            return property;
        }
    }
}
