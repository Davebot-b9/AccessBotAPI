using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado inicial de la conversación.
    /// </summary>
    public class StartState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta inicial del usuario que contiene su nombre.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario, que debe contener su nombre.</param>
        /// <returns>Un mensaje de agradecimiento y una solicitud de la sucursal donde trabaja el usuario.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            conversationState.UserName = response.Message;
            conversationState.CurrentStep = "userBranch";
            return "Gracias ¿En qué sucursal trabajas?";
        }
    }
}