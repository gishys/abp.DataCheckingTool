using System.Collections.Generic;
using System.Linq;

namespace DataCheckingTool.Application.Contracts
{
    public static class DCheckingOptionsExtensions
    {
        public static List<Field> GetFields(this List<Table> tables, string tableName, string fieldName)
        {
            if (tables?.Count > 0)
            {
                var table = tables.FirstOrDefault(d => d.Name == tableName);
                List<Field> fields = table?.Fields;
                if (!string.IsNullOrEmpty(fieldName))
                    fields = table.Fields.Where(d => d.Name == fieldName).ToList();
                if (fields?.Count > 0)
                {
                    foreach (var field in fields)
                    {
                        if (string.IsNullOrEmpty(field.SelectFieldNames))
                        {
                            var tempField = tables.FirstOrDefault(d => d.Name == tableName)?
                                .Fields?.FirstOrDefault(d => d.IsIndex);
                            if (tempField != null)
                                field.SelectFieldNames = tempField.Name;
                        }
                        field.TableName = table.Name;
                    }
                    return fields;
                }
            }
            return null;
        }
    }
}
