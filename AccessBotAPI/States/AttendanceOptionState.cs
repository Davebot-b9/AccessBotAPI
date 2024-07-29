using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja las opciones relacionadas con la asistencia.
    /// </summary>
    public class AttendanceOptionState : IConversationState
    {
        /// <summary>
        /// Maneja el mensaje del usuario y actualiza el estado de la conversación en consecuencia.
        /// </summary>
        /// <param name="conversationState">El estado actual de la conversación.</param>
        /// <param name="response">La respuesta del usuario.</param>
        /// <returns>El siguiente mensaje para el usuario basado en su respuesta.</returns>
        public string HandleMessage(SupportConversationState conversationState, UserResponse response)
        {
            if (string.IsNullOrEmpty(response.Message))
            {
                throw new ArgumentException("No puede estar vacío, elige alguna opción.");
            }
            if (!int.TryParse(response.Message, out int attendanceOption) || attendanceOption < 1 || attendanceOption > 2)
            {
                return "Opción inválida. Por favor ingresa un número correspondiente a una de las opciones presentadas.";
            }

            if (attendanceOption == 1)
            {
                conversationState.CurrentStep = "coordinatorSolution";
                return "Primero comunicate con tu coordinador para que agregue la sucursal que no te aparece en la plataforma.\r\n\r\nSi tu coordinador no ha agregado la sucursal o resuelto el problema hazmelo saber. ¿Se resolvió el problema? (SI | NO)";
            }
            else if (attendanceOption == 2)
            {
                conversationState.CurrentStep = "registerSolution";
                return "Por favor, primero verifica en tu dispositivo si tienes los permisos de la ubicación y que este activada asi como tener buena señal en caso de usar datos o alguna conexión a internet. ¿Se resolvió el problema? (SI | NO)";
            }
            return null;
        }
    }
}
