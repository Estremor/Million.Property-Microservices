namespace Auth.Application.Dto
{
    public record UserDto
    {
        /// <summary>
        /// Nombre de usuaio
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Token de autenticación
        /// </summary>
        public string Token { get; set; }
    }
}
