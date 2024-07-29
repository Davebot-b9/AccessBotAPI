using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja la solicitud y recepción del correo electrónico del usuario.
    /// </summary>
    public class RequestEmailState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario que contiene su correo electrónico.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario, que debe contener su correo electrónico.</param>
        /// <returns>Un mensaje de confirmación indicando que se ha recibido el correo electrónico.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("El correo electrónico no puede estar vacío.");
            }
            conversationState.CurrentStep = "endConversation";
            return "Gracias por proporcionar tu correo electrónico. En breve nos comunicamos contigo para poder darte la mejor solución.";
        }
    }
}