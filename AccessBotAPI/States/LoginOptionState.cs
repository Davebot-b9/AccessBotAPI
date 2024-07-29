using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja las opciones relacionadas con los problemas de inicio de sesión.
    /// </summary>
    public class LoginOptionState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario y determina el siguiente paso en la resolución del problema de inicio de sesión.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en la opción seleccionada por el usuario.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("No puede estar vacío, elige alguna opción.");
            }
            if (!int.TryParse(response.Message, out int loginOption) || loginOption < 1 || loginOption > 2)
            {
                return "Opción inválida. Por favor ingresa un número correspondiente a una de las opciones presentadas.";
            }
            if (loginOption == 1)
            {
                conversationState.CurrentStep = "loginIssueResolution";
                return "Primero revisa tu bandeja de spam, por favor. ¿Encontraste el correo de recuperación de usuario y contraseña? (SI | NO)";
            }
            else if (loginOption == 2)
            {
                conversationState.CurrentStep = "requestEvidence";
                return "Ingresa una imagen o video que muestre el problema ocurrido.";
            }
            return null;
        }
    }
}