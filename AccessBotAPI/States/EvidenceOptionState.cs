using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja las opciones relacionadas con la evidencia proporcionada por el usuario.
    /// </summary>
    public class EvidenceOptionState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario y determina el siguiente paso en la conversación relacionada con la evidencia.
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
            if (!int.TryParse(response.Message, out int evidenceOption) || evidenceOption < 1 || evidenceOption > 2)
            {
                return "Opción inválida. Por favor ingresa un número correspondiente a una de las opciones presentadas.";
            }
            if (evidenceOption == 1)
            {
                conversationState.CurrentStep = "evidenceUploadCheck";
                return "Si la aplicacion te mostro un mensaje como este: ¡Conseguido! La evidencia se ha subido con éxito\", no tienes de que preocuparte, recibimos tu evidencia con exito. ¿Se resolvió el problema? (SI o NO)";
            }
            else if (evidenceOption == 2)
            {
                conversationState.CurrentStep = "evidenceErrorCheck";
                return "Por favor, verifica en tu dispositivo si tienes la ubicación activada asi como tener buena señal en caso de usar datos o alguna conexión a internet. ¿Se resolvió el problema? (SI o NO)";
            }
            return null;
        }
    }
}