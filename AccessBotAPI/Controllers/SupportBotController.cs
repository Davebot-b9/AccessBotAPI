using AccessBotAPI.Models;
using AccessBotAPI.States;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccessBotAPI.Controllers
{
    [ApiController]
    [Route("api/SupportBot")]
    public class SupportBotController : ControllerBase
    {
        /// <summary>
        /// Controlador para manejar las interacciones del chatbot de soporte.
        /// </summary>
        private static Dictionary<string, SupportConversationState> ConversationStates = new Dictionary<string, SupportConversationState>();

        // Mensaje que se presenta al usuario con el menú de opciones
        //private readonly string PresentationMenuMessage = "Escribe el número de la opción que desees:";

        /// <summary>
        /// Maneja las interacciones del chatbot, procesando las respuestas del usuario y avanzando la conversación.
        /// </summary>
        /// <param name="request">La respuesta del usuario, que incluye su ID y mensaje.</param>
        /// <returns>Un IActionResult que contiene la respuesta del chatbot.</returns>
        /// <remarks>
        /// Este método maneja todas las etapas de la conversación, incluyendo:
        /// - Inicio de la conversación
        /// - Captura de información del usuario
        /// - Presentación de opciones de menú
        /// - Resolución de problemas específicos
        /// - Finalización de la conversación
        /// </remarks>
        [HttpPost]
        public IActionResult HandleConversation([FromBody] UserResponse request)
        {
            string responseMessage = "";

            // Verifica si la conversación con el usuario ya ha comenzado
            if (!ConversationStates.ContainsKey(request.UserId))
            {
                ConversationStates[request.UserId] = new SupportConversationState
                {
                    CurrentStep = "start"
                };
                responseMessage = "¡Hola! Soy Kevin de Soporte y será un gusto atenderte. ¿Cuál es tu nombre?";
                return Ok(new { message = responseMessage });
            }

            var conversationState = ConversationStates[request.UserId];

            // Selecciona el manejador de estado correspondiente según el estado actual de la conversación
            IConversationState stateHandler = conversationState.CurrentStep switch
            {
                "start" => new StartState(),
                "userBranch" => new UserBranchState(),
                "menuOptions" => new MenuOptionsState(),
                "loginOptions" => new LoginOptionState(),
                "loginIssueResolution" => new LoginIssueState(),
                "loginIssueCoordinator" => new LoginIssueCoordinatorState(),
                "requestEmail" => new RequestEmailState(),
                "requestEvidence" => new RequestEvidenceState(),
                "evidenceOptions" => new EvidenceOptionState(),
                "evidenceUploadCheck" => new EvidenceUploadCheckState(),
                "evidenceErrorCheck" => new EvidenceErrorCheckState(),
                "attendanceOptions" => new AttendanceOptionState(),
                "coordinatorSolution" => new CoordinatorSolutionState(),
                "registerSolution" => new RegisterSolutionState(),
                "branchAdd" => new BranchAddState(),
                "requestEvidenceConection" => new RequestEvidenceConectionState(),
                "deviceAndroid" => new DeviceAndroidState(),
                "attendanceForm" => new AttendanceFormState(),
                "userSupportMessage" => new UserSupportMessageSate(),
                "endConversation" => new EndConversationState(ConversationStates), // Elimina la conversación
                _ => throw new InvalidOperationException("Estado de conversación no válido.")
            };

            try
            {
                responseMessage = stateHandler.HandleMessage(conversationState, request);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { message = responseMessage });
        }
    }
}
