namespace Properties.Application.Dto
{
    /// <summary>
    /// uso unicamente para realizar filtros con ODTA
    /// </summary>
    public class PropertyReadDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Id unico de la propiedad
        /// </summary>
        public Guid IdProperty { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Direccion
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Codigo interno
        /// </summary>
        public string CodeInternal { get; set; }
        /// <summary>
        /// Año de creacion
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// propietario
        /// </summary>
        public Guid IdOwner { get; set; }
    }

    public class Image
    {
        /// <summary>
        /// Base64 image
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public bool Enabled { get; set; }
    }
}
