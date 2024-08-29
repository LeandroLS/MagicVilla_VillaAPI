using System;

namespace MagicVilla_VillaAPI.Data.Logging;

public interface ILogging
{
    public void log(string messagem, string type);
}
