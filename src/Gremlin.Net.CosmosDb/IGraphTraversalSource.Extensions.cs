﻿using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for <see cref="Gremlin.Net.CosmosDb.IGraphTraversalSource"/> objects
    /// </summary>
    public static class IGraphTraversalSourceExtensions
    {
        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this IGraphTraversalSource graphTraversalSource)
            where TVertex : VertexBase
        {
            var label = LabelNameResolver.GetLabelName(typeof(TVertex));

            return graphTraversalSource.AddV(label).AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <param name="vertex">The vertex to add.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this IGraphTraversalSource graphTraversalSource, TVertex vertex)
            where TVertex : VertexBase
        {
            return AddV(graphTraversalSource, vertex, new JsonSerializerSettings { ContractResolver = new ElementContractResolver() });
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <param name="vertex">The vertex to add.</param>
        /// <param name="serializationSettings">The serialization settings.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this IGraphTraversalSource graphTraversalSource, TVertex vertex, JsonSerializerSettings serializationSettings)
            where TVertex : VertexBase
        {
            var label = LabelNameResolver.GetLabelName(typeof(TVertex));
            var traversal = graphTraversalSource.AddV(label);

            traversal = TraversalHelper.AddObjectProperties(traversal, vertex, serializationSettings);

            return traversal.AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Adds the "E()" step to the traversal.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <param name="edgeIds">The edge id(s).</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<Edge, TEdge> E<TEdge>(this IGraphTraversalSource graphTraversalSource, params object[] edgeIds)
        {
            return graphTraversalSource.E(edgeIds).AsSchemaBound<Edge, TEdge>();
        }

        /// <summary>
        /// Adds the "V()" step to the traversal.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <param name="vertexIds">The vertex id(s).</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<Vertex, TVertex> V<TVertex>(this IGraphTraversalSource graphTraversalSource, params object[] vertexIds)
        {
            return graphTraversalSource.V(vertexIds).AsSchemaBound<Vertex, TVertex>();
        }
    }
}