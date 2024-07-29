using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de solución de problemas del coordinador.
    /// </summary>
    public class CoordinatorSolutionState : IConversationState
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
                conversationState.CurrentStep = "branchAdd";
                return "Por favor proporciona el nombre completo de tu coordinador.";
            }
            return null;
        }
    }
}
