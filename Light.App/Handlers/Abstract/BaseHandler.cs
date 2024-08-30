using Light.Data;
using Microsoft.Extensions.Logging;

namespace Light.App.Handlers.Abstract;

public abstract class BaseHandler
{
    protected readonly SqlContext Context;
    protected readonly ILogger Logger;
    
    public BaseHandler(ILogger<BaseHandler> logger, SqlContext context)
    {
        Logger = logger;
        Context = context;
    }
}