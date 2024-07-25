//using AccessBotAPI.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace AccessBotAPI.Controllers
//{
//    [ApiController]
//    [Route("api/v1/supportbot")]
//    public class SupportBotController : ControllerBase
//    {
//        private static Dictionary<string, SupportConversationState> ConversationStates = new Dictionary<string, SupportConversationState>();
//        private readonly string PresentationMenuMessage = "Escribe el número de la opción que desees:";
//        private readonly string ConversationNotStart = "Conversación no iniciada. Por favor, inicia la conversación primero.";
//        private readonly string UnprocessedOption = "No se puede procesar la opción en el estado actual.";

//        [HttpPost("start")]
//        public IActionResult StartSupportConversation([FromBody] UserResponse request)
//        {
//            if (!ConversationStates.ContainsKey(request.UserId))
//            {
//                ConversationStates[request.UserId] = new SupportConversationState();
//            }

//            var response = new
//            {
//                BotPresentation = "¡Hola! Soy Kevin de Soporte y será un gusto atenderte. ¿Cuál es tu nombre?"
//            };

//            return Ok(response);
//        }

//        [HttpPost("name")]
//        public IActionResult GetUserName([FromBody] UserResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (string.IsNullOrEmpty(response.Message))
//            {
//                return BadRequest("El nombre no puede estar vacío.");
//            }

//            ConversationStates[response.UserId].UserName = response.Message;

//            var NextResponse = new
//            {
//                Message = "Gracias. ¿En qué sucursal trabajas?"
//            };

//            return Ok(NextResponse);
//        }

//        [HttpPost("branch")]
//        public IActionResult GetUserBranch([FromBody] UserResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (string.IsNullOrEmpty(response.Message))
//            {
//                return BadRequest("La sucursal no puede estar vacía.");
//            }

//            ConversationStates[response.UserId].UserBranch = response.Message;

//            var ResponseMenu = new
//            {
//                PresentationMenuMessage,
//                MenuOptions = new[]
//                {
//                    "1. Inicio de sesión.",
//                    "2. Carga de evidencias.",
//                    "3. Registro de asistencias.",
//                    "4. Instalación o actualización de la app.",
//                    "5. Otro"
//                }
//            };

//            return Ok(ResponseMenu);
//        }

//        [HttpPost("option")]
//        public IActionResult HandleOption([FromBody] UserOptionResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (response.Option < 1 || response.Option > 5)
//            {
//                return BadRequest("Opción inválida. Por favor elige una opción entre 1 y 5.");
//            }

//            switch (response.Option)
//            {
//                case 1:
//                    ConversationStates[response.UserId].CurrentStep = "loginOptions";
//                    var LoginOptions = new
//                    {
//                        PresentationMenuMessage,
//                        MenuOptions = new[]
//                        {
//                            "1. Necesito mi usuario y/o contraseña.",
//                            "2. La aplicación me marca error al poner el usuario y contraseña."
//                        }
//                    };
//                    return Ok(LoginOptions);
//                case 2:
//                    ConversationStates[response.UserId].CurrentStep = "evidenceOptions";
//                    var EvidenceOptions = new
//                    {
//                        PresentationMenuMessage,
//                        MenuOptions = new[]
//                        {
//                            "1. No sé si mi evidencia se subió correctamente.",
//                            "2. La aplicación me marca error al tomar la fotografía."
//                        }
//                    };
//                    return Ok(EvidenceOptions);
//                case 3:
//                    ConversationStates[response.UserId].CurrentStep = "attendanceOptions";
//                    var AttendanceOptions = new
//                    {
//                        PresentationMenuMessage,
//                        MenuOptions = new[]
//                        {
//                            "1. No me aparece la tienda donde trabajo.",
//                            "2. Me marca error al registrar mi entrada o salida."
//                        }
//                    };
//                    return Ok(AttendanceOptions);
//                case 4:
//                    ConversationStates[response.UserId].CurrentStep = "details";
//                    var responseMessage = "Seleccionaste Instalación o actualización de la app. Por favor proporciona más detalles.";
//                    return Ok(new { message = responseMessage });
//                case 5:
//                    ConversationStates[response.UserId].CurrentStep = "details";
//                    responseMessage = "Seleccionaste Otro. Por favor proporciona más detalles.";
//                    return Ok(new { message = responseMessage });
//                default:
//                    return BadRequest("Opción inválida. Por favor elige una opción entre 1 y 5.");
//            }
//        }

//        [HttpPost("option/loginissue")]
//        public IActionResult HandleLoginIssue([FromBody] UserSubOptionResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[response.UserId].CurrentStep != "loginOptions")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (response.SubOption == 1)
//            {
//                ConversationStates[response.UserId].CurrentStep = "loginIssueResolution";
//                var responseMessage = "Primero revisa tu bandeja de spam, si eres tan amable por favor. ¿Encontraste el correo de recuperación de usuario y contraseña? (SI o NO)";
//                return Ok(new { message = responseMessage });
//            }
//            else if (response.SubOption == 2)
//            {
//                ConversationStates[response.UserId].CurrentStep = "requestEvidence";
//                var responseMessage = "Ingresa una imagen o video que muestre el problema ocurrido.";
//                return Ok(new { message = responseMessage });
//            }

//            return BadRequest("Subopción inválida. Por favor elige una subopción válida.");
//        }

//        [HttpPost("option/loginissue/resolution")]
//        public IActionResult HandleLoginIssueResolution([FromBody] UserResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[response.UserId].CurrentStep != "loginIssueResolution")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (response.Message.ToLower() == "si")
//            {
//                ConversationStates.Remove(response.UserId);
//                return Ok(new { message = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día." });
//            }
//            else if (response.Message.ToLower() == "no")
//            {
//                ConversationStates[response.UserId].CurrentStep = "loginIssueCoordinator";
//                var responseMessage = "Consulta con tu coordinador si estás dado de alta en la plataforma, ya que desde ahí te manda el correo con tu usuario y contraseña. No olvides revisar la bandeja de spam. ¿Se resolvió el problema? (SI o NO)";
//                return Ok(new { message = responseMessage });
//            }

//            return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//        }

//        [HttpPost("option/loginissue/coordinator")]
//        public IActionResult HandleLoginIssueCoordinator([FromBody] UserResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[response.UserId].CurrentStep != "loginIssueCoordinator")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (response.Message.ToLower() == "si")
//            {
//                ConversationStates.Remove(response.UserId);
//                return Ok(new { message = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día." });
//            }
//            else if (response.Message.ToLower() == "no")
//            {
//                ConversationStates[response.UserId].CurrentStep = "requestEmail";
//                var responseMessage = "Por favor proporciona tu correo electrónico y alguien de soporte se comunicará en breve contigo.";
//                return Ok(new { message = responseMessage });
//            }

//            return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//        }

//        [HttpPost("option/loginissue/email")]
//        public IActionResult HandleRequestEmail([FromBody] UserResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[response.UserId].CurrentStep != "requestEmail")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (string.IsNullOrEmpty(response.Message))
//            {
//                return BadRequest("El correo electrónico no puede estar vacío.");
//            }

//            ConversationStates.Remove(response.UserId);
//            var finalResponseMessage = "Gracias por proporcionar tu correo electrónico. En breve nos comunicamos contigo para poder darte la mejor solución.";
//            return Ok(new { message = finalResponseMessage });
//        }

//        [HttpPost("option/loginissue/evidence")]
//        public IActionResult HandleUploadEvidence([FromBody] EvidenceUploadRequest request)
//        {
//            if (!ConversationStates.ContainsKey(request.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[request.UserId].CurrentStep != "requestEvidence")
//            {
//                return BadRequest(UnprocessedOption);
//            }
//            ConversationStates.Remove(request.UserId);
//            var responseMessage = "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
//            return Ok(new { message = responseMessage });
//        }

//        [HttpPost("option/chargeissue")]

//        public IActionResult HandleChargeEvidenceIssue([FromBody] UserSubOptionResponse response)
//        {
//            if (!ConversationStates.ContainsKey(response.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[response.UserId].CurrentStep != "evidenceOptions")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (response.SubOption == 1)
//            {
//                ConversationStates[response.UserId].CurrentStep = "chargeEvidenceResolution";
//                var responseMessage = "Si la aplicacion te mostro un mensaje como este: ¡Conseguido! La evidencia se ha subido con éxito, no tienes de que preocuparte, recibimos tu evidencia con exito. ¿Se resolvio el problema? (SI o NO)";
//                return Ok(new { message = responseMessage });
//            }
//            else if (response.SubOption == 2)
//            {
//                ConversationStates[response.UserId].CurrentStep = "verifyConection";
//                var responseMessage = "Por favor, verifica en tu dispositivo si tienes la ubicación activada asi como tener buena señal en caso de usar datos o alguna conexión a internet. ¿Se resolvió el problema? (SI o NO)";
//                return Ok(new { message = responseMessage });
//            }

//            return BadRequest("Subopción inválida. Por favor elige una subopción válida.");
//        }

//        [HttpPost("option/chargeissue/resolution")]
//        public IActionResult HandleEvidenceIssueResolution([FromBody] UserResponse request)
//        {
//            if (!ConversationStates.ContainsKey(request.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[request.UserId].CurrentStep != "chargeEvidenceResolution")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (request.Message.Equals("si", StringComparison.CurrentCultureIgnoreCase))
//            {
//                ConversationStates.Remove(request.UserId);
//                return Ok(new { message = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día." });
//            }
//            else if (request.Message.Equals("no", StringComparison.CurrentCultureIgnoreCase))
//            {
//                ConversationStates.Remove(request.UserId);
//                var responseMessage = "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
//                return Ok(new { message = responseMessage });
//            }

//            return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");
//        }

//        [HttpPost("option/chargeissue/verifyconect")]
//        public IActionResult HandleVerifyConect([FromBody] UserResponse request)
//        {
//            if (!ConversationStates.ContainsKey(request.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[request.UserId].CurrentStep != "verifyConection")
//            {
//                return BadRequest(UnprocessedOption);
//            }

//            if (request.Message.ToLower() == "si")
//            {
//                ConversationStates.Remove(request.UserId);
//                return Ok(new { message = "¡Genial! Me alegra haber podido ayudarte. Que tengas un buen día." });
//            }
//            else if (request.Message.ToLower() == "no")
//            {
//                ConversationStates[request.UserId].CurrentStep = "requestImage";
//                var responseMessage = "Ingresa una imagen o video que muestre el problema ocurrido.";
//                return Ok(new { message = responseMessage });
//            }

//            return BadRequest("Respuesta inválida. Por favor responde con SI o NO.");

//        }

//        [HttpPost("option/chargeissue/verifyconect/evidence")]
//        public IActionResult HandleEvidence([FromBody] EvidenceUploadRequest request)
//        {
//            if (!ConversationStates.ContainsKey(request.UserId))
//            {
//                return BadRequest(ConversationNotStart);
//            }

//            if (ConversationStates[request.UserId].CurrentStep != "requestImage")
//            {
//                return BadRequest(UnprocessedOption);
//            }
//            ConversationStates.Remove(request.UserId);
//            var responseMessage = "En breve nos comunicamos contigo para resolver esta incidencia, agradecemos mucho tu paciencia.";
//            return Ok(new { message = responseMessage });
//        }
//    }
//}
