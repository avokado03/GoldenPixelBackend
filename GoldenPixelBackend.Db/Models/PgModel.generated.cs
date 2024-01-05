﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : GP
	/// Data Source    : tcp://localhost:5432
	/// Server Version : 12.4
	/// </summary>
	public partial class GPDB : LinqToDB.Data.DataConnection
	{
		public ITable<Request> Requests { get { return this.GetTable<Request>(); } }

		partial void InitMappingSchema()
		{
		}

		public GPDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public GPDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public GPDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public GPDB(DataOptions<GPDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="public", Name="requests")]
	public partial class Request
	{
		[Column("id"),          NotNull] public Guid   Id          { get; set; } // uuid
		[Column("email"),       NotNull] public string Email       { get; set; } // character varying(100)
		[Column("requester"),   NotNull] public string Requester   { get; set; } // character varying(50)
		[Column("description"), NotNull] public string Description { get; set; } // character varying(500)
	}
}