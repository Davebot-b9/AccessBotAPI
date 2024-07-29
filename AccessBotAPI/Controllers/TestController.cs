//using AccessBotAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using AccessBotAPI.States;
//using System.Collections.Generic;

//namespace AccessBotAPI.Controllers
//{
//    [ApiController]
//    [Route("api/SupportBot")]
//    public class SupportBotController : ControllerBase
//    {
//        /// <summary>
//        /// Controlador para manejar las interacciones del chatbot de soporte.
//        /// </summary>
//        private static readonly Dictionary<string, SupportConversationState> ConversationStates = new Dictionary<string, SupportConversationState>();
//        private readonly string PresentationMenuMessage = "Escribe el número de la opción que desees:";
//        private readonly string UnprocessedOption = "Opción inválida. Por favor elige una subopción válida.";

//        /// <summary>
//        /// Maneja las interacciones del chatbot, procesando las respuestas del usuario y avanzando la conversación.
//        /// </summary>
//        /// <param name="request">La respuesta del usuario, que incluye su ID y mensaje.</param>
//        /// <returns>Un IActionResult que contiene la respuesta del chatbot.</returns>
//        /// <remarks>
//        /// Este método maneja todas las etapas de la conversación, incluyendo:
//        /// - Inicio de la conversación
//        /// - Captura de información del usuario
//        /// - Presentación de opciones de menú
//        /// - Resolución de problemas específicos
//        /// - Finalización de la conversación
//        /// </remarks>
//        [HttpPost]
//        public IActionResult HandleConversation([FromBody] UserResponse request)
//        {
//            string responseMessage = "";

//            if (!ConversationStates.ContainsKey(request.UserId))
//            {
//                ConversationStates[request.UserId] = new SupportConversationState
//                {
//                    CurrentStep = "start"
//                };
//                responseMessage = "¡Hola! Soy Kevin de Soporte y será un gusto atenderte. ¿Cuál es tu nombre?";
//                return Ok(new { message = responseMessage });
//            }

//            var conversationState = ConversationStates[request.UserId];

//            switch (conversationState.CurrentStep)
//            {
//                case "start":
//                    if (string.IsNullOrEmpty(request.Message))
//                    {
//                        return BadRequest("El nombre no puede estar vacío.");
//                    }
//                    conversationState.UserName = request.Message;
//                    conversationState.CurrentStep = "getUserBranch";
//                    responseMessage = "Gracias. ¿En qué sucursal trabajas?";
//                    break;

//                case "getUserBranch":
//                    if (string.IsNullOrEmpty(request.Message))
//                    {
//                        return BadRequest("La sucursal no puede estar vacía.");
//                    }
//                    conversationState.UserBranch = request.Message;
//                    conversationState.CurrentStep = "menuOptions";
//                    responseMessage = PresentationMenuMessage;
//                    return Ok(new
//                    {
//                        message = responseMessage,
//                        menuOptions = new[]
//                        {
//                            "1. Inicio de sesión.",
//                            "2. Carga de evidencias.",
//                            "3. Registro de asistencias.",
//                            "4. Instalación o actualización de la app.",
//                            "5. Otro"
//                        }
//                    });

//                case "menuOptions":
//                    if (!int.TryParse(request.Message, out int option) || option < 1 || option > 5)
//                    {
//                        return BadRequest("Opción inválida. Por favor elige una opción entre 1 y 5.");
//                    }
//                    switch (option)
//                    {
//                        case 1:
//                            conversationState.CurrentStep = "loginOptions";
//                            responseMessage = PresentationMenuMessage;
//                            return Ok(new
//                            {
//                                message = responseMessage,
//                                menuOptions = new[]
//                                {
//                                    "1. Necesito mi usuario y/o contraseña.",
//                                    "2. La aplicación me marca error al poner el usuario y contraseña."
//                                }
//                            });
//                        case 2:
//                            conversationState.CurrentStep = "evidenceOptions";
//                            responseMessage = PresentationMenuMessage;
//                            return Ok(new
//                            {
//                                message = responseMessage,
//                                menuOptions = new[]
//                                {
//                                    "1. No sé si mi evidencia se subió correctamente.",
//                                    "2. La aplicación me marca error al tomar la fotografía."
//                                }
//                            });
//                        case 3:
//                            conversationState.CurrentStep = "attendanceOptions";
//                            responseMessage = PresentationMenuMessage;
//                            return Ok(new
//                            {
//                                message = responseMessage,
//                                menuOptions = new[]
//                                {
//                                    "1. No me aparece la tienda donde trabajo.",
//                                    "2. Me marca error al registrar mi entrada o salida."
//                                }
//                            });
//                        case 4:
//                            conversationState.CurrentStep = "deviceAndroid";
//                            responseMessage = "¿Tu dispositivo es Android? (SI | NO)";
//                            break;
//                        case 5:
//                            conversationState.CurrentStep = "userSupportMessage";
//                            responseMessage = "Cuentame un poco más acerca del problema que tienes si eres tan amable.";
//                            break;
//                    }
//                    break;

//                case "loginOptions":
//                    if (!int.TryParse(request.Message, out int loginOption) || loginOption < 1 || loginOption > 2)
//                    {
//                        return BadRequest(UnprocessedOption);
//                    }
//                    if (loginOption == 1)
//                    {
//                        conversationState.CurrentStep = "loginIssueResolution";
//                        responseMessage = "Primero revisa tu bandeja de spam, si eres tan amable por favor. ¿Encontraste el correo de recuperación de usuario y contraseña? (SI | NO)";
//                    }
//                    else if (loginOption == 2)
//                    {
//                        conversationState.CurrentStep = "requestEvidence";
//                        responseMessage = "Ingresa una imagen o video que muestre el problema ocurrido.";
//                    }
//                    break;

//                case "loginIssueResolution":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "loginIssueCoordinator";
//                        responseMessage = "Consulta con tu coordinador si estás dado de alta en la plataforma, ya que desde ahí te manda el correo con tu usuario y contraseña. No olvides revisar la bandeja de spam. ¿Se resolvió el problema? (SI o NO)";
//                    }
//                    else
//                    {
//                        return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//                    }
//                    break;

//                case "loginIssueCoordinator":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "requestEmail";
//                        responseMessage = "Por favor proporciona tu correo electrónico y alguien de soporte se comunicará en breve contigo.";
//                    }
//                    else
//                    {
//                        return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//                    }
//                    break;

//                case "requestEmail":
//                    if (string.IsNullOrEmpty(request.Message))
//                    {
//                        return BadRequest("El correo electrónico no puede estar vacío.");
//                    }
//                    ConversationStates.Remove(request.UserId);
//                    responseMessage = "Gracias por proporcionar tu correo electrónico. En breve nos comunicamos contigo para poder darte la mejor solución.";
//                    break;

//                case "evidenceOptions":
//                    if (!int.TryParse(request.Message, out int evidenceOption) || evidenceOption < 1 || evidenceOption > 2)
//                    {
//                        return BadRequest(UnprocessedOption);
//                    }
//                    if (evidenceOption == 1)
//                    {
//                        conversationState.CurrentStep = "evidenceUploadCheck";
//                        responseMessage = "Si la aplicacion te mostro un mensaje como este: ¡Conseguido! La evidencia se ha subido con éxito\", no tienes de que preocuparte, recibimos tu evidencia con exito. ¿Se resolvió el problema? (SI o NO)";
//                    }
//                    else if (evidenceOption == 2)
//                    {
//                        conversationState.CurrentStep = "evidenceErrorCheck";
//                        responseMessage = "Por favor, verifica en tu dispositivo si tienes la ubicación activada asi como tener buena señal en caso de usar datos o alguna conexión a internet. ¿Se resolvió el problema? (SI o NO)";
//                    }
//                    break;

//                case "evidenceUploadCheck":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "supportContact";
//                        responseMessage = "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
//                    }
//                    else
//                    {
//                        return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//                    }
//                    break;

//                case "evidenceErrorCheck":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día.";
//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "requestEvidence";
//                        responseMessage = "Ingresa una imagen o video que muestre el problema ocurrido.";
//                    }
//                    else
//                    {
//                        return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//                    }
//                    break;

//                case "requestEvidence":
//                    if (string.IsNullOrEmpty(request.Message))
//                    {
//                        return BadRequest("La evidencia no puede estar vacía.");
//                    }
//                    ConversationStates.Remove(request.UserId);
//                    responseMessage = "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
//                    break;

//                case "attendanceOptions":
//                    if (!int.TryParse(request.Message, out int attendanceOption) || attendanceOption < 1 || attendanceOption > 2)
//                    {
//                        return BadRequest(UnprocessedOption);
//                    }

//                    if (attendanceOption == 1)
//                    {
//                        conversationState.CurrentStep = "coordinatorSolution";
//                        responseMessage = "Primero comunicate con tu coordinador para que agregue la sucursal que no te aparece en la plataforma.\r\n\r\nSi tu coordinador no ha agregado la sucursal o resuelto el problema hazmelo saber. ¿Se resolvió el problema? (SI | NO)";
//                    }
//                    else if (attendanceOption == 2)
//                    {
//                        conversationState.CurrentStep = "registerSolution";
//                        responseMessage = "Por favor, primero verifica en tu dispositivo si tienes los permisos de la ubicación y que este activada asi como tener buena señal en caso de usar datos o alguna conexión a internet. ¿Se resolvió el problema? (SI | NO)";
//                    }
//                    break;

//                case "coordinatorSolution":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "Gracias por comunicarte al area de soporte a usuarios. ¡Ten un excelente día!";

//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "branchAdd";
//                        responseMessage = "Por favor proporciona el nombre completo de tu coordinador.";
//                    }
//                    break;

//                case "branchAdd":
//                    if (string.IsNullOrEmpty(request.Message))
//                    {
//                        return BadRequest("La sucursal faltante no puede estar vacía.");
//                    }
//                    conversationState.CurrentStep = "userSupportMessage";
//                    responseMessage = "Ingresa la sucursal que no te aparece por favor.";
//                    break;

//                case "registerSolution":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "Gracias por comunicarte al area de soporte a usuarios. ¡Ten un excelente día!";

//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "requestConectImage";
//                        responseMessage = "Ingresa una imagen o video que muestre el problema ocurrido.";
//                    }
//                    break;

//                case "requestConectImage":
//                    if (string.IsNullOrEmpty(request.Message))
//                    {
//                        return BadRequest("La evidencia no puede estar vacía");
//                    }
//                    conversationState.CurrentStep = "userSupportMessage";
//                    responseMessage = "¡Perfecto! muchas gracias, ahora si eres tan amable de proporcionar las especificaciones de tu dispositivo por favor.";
//                    break;

//                case "deviceAndroid":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        conversationState.CurrentStep = "finalMessage";
//                        responseMessage = "¡Ya tenemos nuestra aplicacion en PlayStore, te comparto el link para que vayas y la instales. url";
//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "formAsis";
//                        responseMessage = "Si tu dispositivo no cuenta con Android, te comparto el siguiente link para que puedas marcar tus asistencias. url ¿Se resolvió tu problema? (SI | NO)";
//                    }
//                    else
//                    {
//                        return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//                    }
//                    break;

//                case "formAsistant":
//                    if (request.Message.ToLower() == "si")
//                    {
//                        ConversationStates.Remove(request.UserId);
//                        responseMessage = "Gracias por comunicarte al area de soporte a usuarios. ¡Ten un excelente día!";
//                    }
//                    else if (request.Message.ToLower() == "no")
//                    {
//                        conversationState.CurrentStep = "userSupportMessage";
//                        responseMessage = "Cuentame un poco más acerca del problema que tienes si eres tan amable.";
//                    }
//                    else
//                    {
//                        return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//                    }
//                    break;

//                case "finalMessage":
//                    ConversationStates.Remove(request.UserId);
//                    responseMessage = "Gracias por comunicarte al area de soporte a usuarios. ¡Ten un excelente día!";
//                    break;

//                case "userSupportMessage":
//                    ConversationStates.Remove(request.UserId);
//                    responseMessage = "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
//                    break;

//                default:
//                    return BadRequest("Opción no válida.");
//            }

//            return Ok(new { message = responseMessage });
//        }
//    }
//}
