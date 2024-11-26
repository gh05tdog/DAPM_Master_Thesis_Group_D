using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLibrary.Models
{
    public class Resource
    {
        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
        public Guid? ResourceId { get; set; }
        public string? Name { get; set; }
    }
   
    public class Handle
    {
        public string? type { get; set; }
        public string? Id { get; set; }
    }
    public class Algorithm
    {
        public Guid? OrganizationId { get; set; }
        public Guid? RepositoryId { get; set; }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
    public class RepositoryData
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public Guid? organizationId { get; set; }
    }

    public class RepositoryTransferData
    {
        public RepositoryData? Repository { get; set; }
        public string? name { get; set; }
        
    }
    public class OrganizationData
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? domain { get; set; }
    }
    
    public class InstantiationData
    {
        public Resource Resource { get; set; }
        public RepositoryTransferData Repository { get; set; }
        public OrganizationData Organization { get; set; }
        public Algorithm Algorithm { get; set; }
    }
    public class TemplateData
    {
        public IEnumerable<Handle> SourceHandles { get; set; } = new List<Handle>();
        public IEnumerable<Handle> TargetHandles { get; set; } = new List<Handle>();
        public string? Hint { get; set; }
    }
    public class Edge
    {
        public string? source { get; set; }
        public string? target { get; set; }
        public string? SourceHandle { get; set; }
        public string? TargetHandle { get; set; }
        
    }
    
    public class NodePosition
    {
        public float? X { get; set; }
        public float? Y { get; set; }
    }

    public class NodeData
    {
        public string? Label { get; set; }
        public TemplateData? TemplateData { get; set; }
        public InstantiationData? InstantiationData { get; set; }
    }

    public class Node
    {
        public string? Id { get; set;} 
        public string? Type { get; set; }
        public NodePosition? Position { get; set; }
        public NodeData? Data { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
    }
    public class Pipeline
    {
        public IEnumerable<Node> Nodes { get; set; } = new List<Node>();
        public IEnumerable<Edge> Edges { get; set; } = new List<Edge>();
    }

    public class PipelineDTO
    {
        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Pipeline? Pipeline { get; set; }
    }
}
