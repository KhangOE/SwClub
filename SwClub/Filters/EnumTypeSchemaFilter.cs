namespace SwClub.Web.Filters
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class EnumTypeSchemaFilter : ISchemaFilter
    {
        private readonly XDocument _xmlComments;

        public EnumTypeSchemaFilter(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                this._xmlComments = XDocument.Load(xmlPath);
            }
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (this._xmlComments == null)
            {
                return;
            }

            if (schema.Enum != null
                && schema.Enum.Count > 0
                && context.Type != null
                && context.Type.IsEnum)
            {
                schema.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(name => schema.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))} - {name}")));

                schema.Description += "<p>Members:</p><ul>";

                var enumMemberNames = schema.Enum.OfType<OpenApiString>().Select(v => v.Value).ToList();
                if (enumMemberNames.Count > 0)
                {
                    schema.Enum.Clear();
                    Enum.GetNames(context.Type)
                        .ToList()
                        .ForEach(name => schema.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))}")));

                    var fullTypeName = context.Type.FullName;
                    foreach (var enumMemberName in enumMemberNames)
                    {
                        var fullEnumMemberName = $"F:{fullTypeName}.{enumMemberName}";

                        var enumMemberComments = this._xmlComments.Descendants("member")
                            .FirstOrDefault(m => m.Attribute("name").Value.Equals(fullEnumMemberName, StringComparison.OrdinalIgnoreCase));
                        if (enumMemberComments == null)
                        {
                            schema.Description += $"<li><i>{enumMemberName}</i></ li >";
                            continue;
                        }

                        var summary = enumMemberComments.Descendants("summary").FirstOrDefault();
                        if (summary == null)
                        {
                            schema.Description += $"<li><i>{enumMemberName}</i></ li >";
                            continue;
                        }

                        schema.Description += $"<li><i>{enumMemberName}</i> - {summary.Value.Trim()}</ li >";
                    }
                }
                else
                {
                    var enums = Enum.GetNames(context.Type).ToList();
                    foreach (var item in enums)
                    {
                        var openApiString = new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, item))} - {item}");
                        schema.Description += $"<li><i>{openApiString.Value}</i></ li >";
                    }
                }

                schema.Description += "</ul>";
            }
        }
    }
}
