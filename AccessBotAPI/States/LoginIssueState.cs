using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja los problemas generales de inicio de sesión.
    /// </summary>
    public class LoginIssueState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario respecto a si el problema de inicio de sesión se resolvió.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en si el problema se resolvió o no.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("Respuesta inválida. Por favor responde con SI o NO.");
            }
            if (response.Message.ToLower() == "si")
            {
                conversationState.CurrentStep = "endConversation";
                return "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "loginIssueCoordinator";
                return "Consulta con tu coordinador si estás dado de alta en la plataforma, ya que desde ahí te manda el correo con tu usuario y contraseña. No olvides revisar la bandeja de spam. ¿Se resolvió el problema? (SI o NO)";
            }
            return null;
        }
    }
}