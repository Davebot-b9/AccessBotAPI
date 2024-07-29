using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de la conversación relacionado con dar soporte especial con agente humano al usuario.
    /// </summary>
    public class UserSupportMessageSate: IConversationState
    {
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            conversationState.CurrentStep = "endConversation";
            return"En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
        }
    }
}
