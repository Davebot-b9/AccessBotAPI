using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de la conversación relacionado con la sucursal del usuario.
    /// </summary>
    public class UserBranchState : IConversationState
    {
        /// <summary>
        /// Procesa la respuesta del usuario que contiene la sucursal donde trabaja.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario, que debe contener la sucursal donde trabaja.</param>
        /// <returns>Un mensaje con las opciones del menú principal.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("La sucursal no puede estar vacía");
            }
            conversationState.UserBranch = response.Message;
            conversationState.CurrentStep = "menuOptions";
            string[] menuOptions =
            {
                "1. Inicio de sesión.",
                "2. Carga de evidencias.",
                "3. Registro de asistencias.",
                "4. Instalación o actualización de la app.",
                "5. Otro"
            };
            var menuMessage = "Escribe el número de la opción que desees:\n" + string.Join("\n", menuOptions);
            return menuMessage;
        }
    }
}