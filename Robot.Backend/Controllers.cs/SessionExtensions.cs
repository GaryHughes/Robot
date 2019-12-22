using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Robot.Backend
{
    // Convenience methods for reading/writing the Robot state from the session.
    public static class SessionExtensions
    {
        const string RobotKey = "Robot";

        readonly static JsonSerializerOptions Options = new JsonSerializerOptions
        { 
            Converters = { new RobotJsonConverter() },
            WriteIndented = false
        };

        public static void Set(this ISession session, Robot robot)
        {
            session.SetString(RobotKey, JsonSerializer.Serialize(robot, Options));
        }

        public static Robot? Get(this ISession session)
        {
            var json = session.GetString(RobotKey);
            if (string.IsNullOrEmpty(json)) {
                return null;
            }
            return JsonSerializer.Deserialize<Robot>(json, Options);
        }
    }
}