using AccessBotAPI.Models;
namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja las opciones del menú principal de soporte.
    /// </summary>
    public class MenuOptionsState : IConversationState
    {
        private readonly string OptionMenu = "Escribe el numero de la opcion que desees: \n";

        /// <summary>
        /// Procesa la selección del usuario en el menú principal y determina el siguiente paso en la conversación.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación de soporte.</param>
        /// <param name="response">La respuesta proporcionada por el usuario.</param>
        /// <returns>Un mensaje de respuesta basado en la opción seleccionada por el usuario.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (!int.TryParse(response.Message, out int option) || option < 1 || option > 5)
            {
                throw new ArgumentException("Opcion invalida. Por favor elige una opcion entre 1 y 5.");
            }
            var LoginOptions = new[]
            {
                "1. Necesito mi usuario y/o contraseña.",
                "2. La aplicación me marca error al poner el usuario y contraseña."
            };
            var EvidenceOptions = new[]
            {
                "1. No sé si mi evidencia se subió correctamente.",
                "2. La aplicación me marca error al tomar la fotografía."
            };
            var AttendaceOptions = new[]
            {
                "1. No me aparece la tienda donde trabajo.",
                "2. Me marca error al registrar mi entrada o salida."
            };
            var menuLoginOptions = OptionMenu + string.Join("\n", LoginOptions);
            var menuEvidenceOptions = OptionMenu + string.Join("\n", EvidenceOptions);
            var menuAttendaceOptions = OptionMenu + string.Join("\n", AttendaceOptions);
            switch (option)
            {
                case 1:
                    conversationState.CurrentStep = "loginOptions";
                    return menuLoginOptions;
                case 2:
                    conversationState.CurrentStep = "evidenceOptions";
                    return menuEvidenceOptions;
                case 3:
                    conversationState.CurrentStep = "attendanceOptions";
                    return menuAttendaceOptions;
                case 4:
                    conversationState.CurrentStep = "deviceAndroid";
                    return "¿Tu dispositivo es Android? (SI | NO)";
                case 5:
                    conversationState.CurrentStep = "userSupportMessage";
                    return "Cuentame un poco más acerca del problema que tienes si eres tan amable.";
            }
            return null;
        }
    }
}