namespace Properties.Application.Dto
{
    public class ImageDto
    {
        /// <summary>
        /// Base64 image
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Codigo interno de la propiedad
        /// </summary>
        public string InernalCode { get; set; }
        /// <summary>
        /// stado activo / inactivo
        /// </summary>
        public bool Enabled { get; set; }
    }
}
