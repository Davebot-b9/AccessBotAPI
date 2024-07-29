using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado del formulario de asistencia en la conversación.
    /// </summary>
    public class AttendanceFormState : IConversationState
    {
        /// <summary>
        /// Maneja el mensaje del usuario y actualiza el estado de la conversación en consecuencia.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación.</param>
        /// <param name="response">La respuesta del usuario.</param>
        /// <returns>El siguiente mensaje para el usuario basado en su respuesta.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (response.Message.ToLower() == "si")
            {
                conversationState.CurrentStep = "endConversation";
                return "Gracias por comunicarte al área de soporte a usuarios. ¡Ten un excelente día!";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "userSupportMessage";
                return "Cuéntame un poco más acerca del problema que tienes si eres tan amable.";
            }
            else
            {
                return "Respuesta inválida. Por favor responde con SI o NO.";
            }
        }
    }
}
