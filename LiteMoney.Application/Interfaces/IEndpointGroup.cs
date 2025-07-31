using Microsoft.AspNetCore.Builder;

namespace LiteMoney.Application.Interfaces;

public interface IEndpointGroup
{
    void Map(WebApplication app);
}
