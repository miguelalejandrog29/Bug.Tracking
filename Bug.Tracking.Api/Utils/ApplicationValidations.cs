namespace Bug.Tracking.Api.Utils
{
    public static class ApplicationValidations
    {
        /// <summary>
        /// Verifica que al menos una propiedad de la Entidad no sea nula o vacía
        /// </summary>
        public static bool EntityWithAtLeastOneProperty(Object entity)
        {
            var properties = entity.GetType().GetProperties();
            var existValue = false;

            foreach (var property in properties)
            {
                var value = property.GetValue(entity)?.ToString();
                if (!string.IsNullOrEmpty(value)) existValue = true;
            }

            return existValue;
        }
    }
}
