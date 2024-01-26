
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CiqonaCompiling.Translation.Abstraction.AbstractedProgramMembers
{
	internal class Datatypes
	{
		private readonly Dictionary<string, Datatype> dict;
		private uint numClientTypes;

		public Datatypes()
		{
			this.dict = new()
			{
				{"void", new DatatypeVoid()},
				{"int", new DatatypePrimitive("int32_t", 4)}
			};

			this.numClientTypes = 0;
		}

		public bool Has(string name)
		{ return this.dict.ContainsKey(name); }

		public Datatype Get(string name)
		{ return this.dict[name]; }

		public void AddNewClientType(string name, ImmutableArray<DatatypeReal> fields)
		{
			if (this.dict.ContainsKey(name))
			{ throw new ArgumentException("Tried to add a new client type to the dictionary that was already added."); }

			this.numClientTypes++;

			this.dict.Add(name, new DatatypeClient(numClientTypes, name, fields));
		}
	}

	internal abstract class Datatype
	{
		public readonly string actualizedCName;

		public Datatype(string actualizedCName)
		{
			this.actualizedCName = actualizedCName;
		}
	}

	internal class DatatypeVoid : Datatype
	{
		public DatatypeVoid() : base("void") {}
	}

	internal abstract class DatatypeReal : Datatype
	{
		public DatatypeReal(string actualizedCName) : base(actualizedCName) {}
	}

	internal class DatatypePrimitive : DatatypeReal
	{
		public readonly byte size;

		public DatatypePrimitive(string actualizedCName, byte size) : base(actualizedCName)
		{
			this.size = size;
		}
	}

	internal class DatatypeClient : DatatypeReal
	{
		public readonly uint actualizedId;
		public readonly string name;
		public readonly ImmutableArray<DatatypeReal> fields;

		public DatatypeClient(uint actualizedId, string name, ImmutableArray<DatatypeReal> fields) : base(ToActualizedCName(actualizedId))
		{
			this.actualizedId = actualizedId;
			this.name = name;
			this.fields = fields;
		}

		private static string ToActualizedCName(uint actualizedId)
		{
			return $"ciq_type_client_{actualizedId}";
		}
	}
}
