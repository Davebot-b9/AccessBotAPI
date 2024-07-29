using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja la solicitud y recepción de evidencia de conexión del usuario.
    /// </summary>
    public class RequestEvidenceConectionState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario que contiene la evidencia de conexión.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario, que debe contener la evidencia de conexión.</param>
        /// <returns>Un mensaje de confirmación y una solicitud de especificaciones del dispositivo del usuario.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                return "La evidencia no puede estar vacía";
            }
            conversationState.CurrentStep = "userSupportMessage";
            return "¡Perfecto! muchas gracias, ahora si eres tan amable de proporcionar las especificaciones de tu dispositivo por favor.\n" + "Por ejemplo: Si es Samsung S23, Xiaomi Redmi Note 10, o el que sea que tengas, si es actual o es un dispositivo no compatible, si usa Android 13 o una version más antigua.";
        }
    }
}