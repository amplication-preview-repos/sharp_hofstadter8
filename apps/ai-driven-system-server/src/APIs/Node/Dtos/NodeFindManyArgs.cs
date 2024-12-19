using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class NodeFindManyArgs : FindManyInput<Node, NodeWhereInput> { }
