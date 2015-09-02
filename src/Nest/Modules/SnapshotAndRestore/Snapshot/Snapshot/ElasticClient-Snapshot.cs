﻿using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// A repository can contain multiple snapshots of the same cluster. Snapshot are identified by unique names within the cluster.
		/// /// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The name of the repository we want to create a snapshot in</param>
		/// <param name="snapshotName">The name of the snapshot</param>
		/// <param name="selector">Optionally provide more details about the snapshot operation</param>
		ISnapshotResponse Snapshot(string repository, string snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc/>
		ISnapshotResponse Snapshot(ISnapshotRequest snapshotRequest);

		/// <inheritdoc/>
		Task<ISnapshotResponse> SnapshotAsync(string repository, string snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc/>
		Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest snapshotRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISnapshotResponse Snapshot(string repository, string snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) => 
			this.Dispatcher.Dispatch<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse>(
				selector.InvokeOrDefault(new SnapshotDescriptor().Snapshot(snapshotName).Repository(repository)),
				this.LowLevelDispatch.SnapshotCreateDispatch<SnapshotResponse>
			);

		/// <inheritdoc/>
		public ISnapshotResponse Snapshot(ISnapshotRequest snapshotRequest) => 
			this.Dispatcher.Dispatch<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse>(
				snapshotRequest,
				this.LowLevelDispatch.SnapshotCreateDispatch<SnapshotResponse>
			);

		/// <inheritdoc/>
		public Task<ISnapshotResponse> SnapshotAsync(string repository, string snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector = null) => 
			this.Dispatcher.DispatchAsync<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				selector.InvokeOrDefault(new SnapshotDescriptor().Snapshot(snapshotName).Repository(repository)),
				this.LowLevelDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>
			);

		/// <inheritdoc/>
		public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest snapshotRequest) => 
			this.Dispatcher.DispatchAsync<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				snapshotRequest,
				this.LowLevelDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>
			);
	}
}