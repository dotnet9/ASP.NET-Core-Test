using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Authors;

public class Author : FullAuditedAggregateRoot<Guid>
{
	private Author()
	{
		/* This constructor is for deserialization / ORM purpose */
	}

	internal Author(
		Guid id,
		[NotNull] string name,
		DateTime birthDate,
		[CanBeNull] string shortBio = null)
		: base(id)
	{
		SetName(name);
		BirthDate = birthDate;
		ShortBio = shortBio;
	}

	public string Name { get; private set; }
	public DateTime BirthDate { get; set; }
	public string ShortBio { get; set; }

	internal Author ChangeName([NotNull] string name)
	{
		SetName(name);
		return this;
	}

	private void SetName([NotNull] string name)
	{
		Name = Check.NotNullOrWhiteSpace(
			name,
			nameof(name),
			AuthorConsts.MaxNameLength
		);
	}
}