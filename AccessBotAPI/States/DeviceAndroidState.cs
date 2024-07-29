﻿using AccessBotAPI.Models;

namespace AccessBotAPI.States
{
    /// <summary>
    /// Maneja el estado de verificación de dispositivos Android.
    /// </summary>
    public class DeviceAndroidState : IConversationState
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
                return "¡Ya tenemos nuestra aplicación en PlayStore, te comparto el link para que vayas y la instales. url\n" + "¡Ten un excelente día!";
            }
            else if (response.Message.ToLower() == "no")
            {
                conversationState.CurrentStep = "attendanceForm";
                return "Si tu dispositivo no cuenta con Android, te comparto el siguiente link para que puedas marcar tus asistencias. url\n ¿Se resolvió tu problema? (SI | NO)";
            }
            else
            {
                return "Respuesta inválida. Por favor responde con SI o NO.";
            }
        }
    }
}