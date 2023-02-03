using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Store.Data.EF
{
	public class StoreDbContext : DbContext
	{
		public DbSet<BookDto> Books { get; set; }
		public DbSet<OrderDto> Orders { get; set; }
		public DbSet<OrderItemDto> Items { get; set; }

		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			BuildBooks(modelBuilder);
			BuildOrders(modelBuilder);
			BuildOrderItems(modelBuilder);
		}

		private void BuildOrderItems(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderItemDto>(action =>
			{
				action.Property(dto => dto.Price)
						  .HasColumnType("money");

				action.HasOne(dto => dto.Order)
					  .WithMany(dto => dto.Items)
					  .IsRequired();
			});
		}

		private static void BuildOrders(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderDto>(action =>
			{
				action.Property(dto => dto.CellPhone)
					  .HasMaxLength(20);

				action.Property(dto => dto.DeliveryUniqueCode)
					  .HasMaxLength(40);

				action.Property(dto => dto.DeliveryPrice)
					  .HasColumnType("money");

				action.Property(dto => dto.DeliveryParameters)
					  .HasConversion(
						  value => JsonConvert.SerializeObject(value),
						  value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
					  .Metadata.SetValueComparer(DictionaryComparer);

				action.Property(dto => dto.PaymentParameters)
					  .HasConversion(
						  value => JsonConvert.SerializeObject(value),
						  value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
					  .Metadata.SetValueComparer(DictionaryComparer);
			});
		}

		private static readonly ValueComparer DictionaryComparer =
			new ValueComparer<Dictionary<string, string>>((dictionary1, dictionary2) =>
														   dictionary1.SequenceEqual(dictionary2),
															dictionary => dictionary.Aggregate(0, (a, p) =>
															HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
														   )
														  );

		private static void BuildBooks(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BookDto>(action =>
			{
				action.Property(dto => dto.Isbn)
					  .HasMaxLength(17)
					  .IsRequired();

				action.Property(dto => dto.Title)
					  .IsRequired();

				action.Property(dto => dto.Price)
					  .HasColumnType("money");
				action.HasData(
					new BookDto
					{
						Id = 1,
						Isbn = "ISBN0201038013",
						Author = "D. Knuth",
						Title = "Art Of Programming",
						Description = "Первый том серии книг Искусство программирования начинается с описания основных понятий и методов программирования. Затем автор сосредотачивается на рассмотрении информационных структур - представлении информации внутри компьютера, структурных связях между элементами данных и о способам эффективной работы с ними. Для методов имитации, символьных вычислений, числовых методов, методов разработки программного обеспечения даны примеры элементарных приложений. По сравнению с предыдущим изданием, добавлены десятки простых, но в то же время очень важных алгоритмов. В соответствии с современными направлениями исследований был существенно переработан также раздел математического введения.",
						Price = 490m
					},
					new BookDto
					{
						Id = 2,
						Isbn = "ISBN0201485672",
						Author = "M. Fowler",
						Title = "Refactoring",
						Description = "Помимо описания различных методов рефакторинга, автор приводит подробный каталог более чем с семьюдесятью рефакторингами и полезными указаниями, которые научат вас, когда их следует применять. Книга содержит подробное описание свыше 70 методов рефакторинга, причем не только теоретическое их описание, но и практические примеры на языке программирования Java. Следует учесть, что изложенные в книге идеи применимы к любому объектно-ориентированному языку программирования.",
						Price = 2250m
					},
					new BookDto
					{
						Id = 3,
						Isbn = "ISBN0131101633",
						Author = "B. W. Kernighan, D. M. Ritchie",
						Title = "C Programming Language",
						Description = "Перед Вами классическая книга по языку программирования C (Си), написанная самими разработчиками этого языка и выдержавшая в США уже 34 переиздания! Книга является как практически исчерпывающим справочником, так и учебным пособием по самому распространенному языку программирования C (Си). Предлагаемое второе издание книги было существенно переработано по сравнению с первым в связи с появлением стандарта ANSI C, для которого она частично послужила основой. Книга Язык программирования C (Си) не рекомендуется для чтения новичкам; для своего изучения она требует знания основ программирования и компьютеров Книга Язык программирования C (Си) предназначена для широкого круга программистов и компьютерных специалистов. Книга может использоваться как учебное пособие для высших учебных заведений. переработанное и дополненное.",
						Price = 1775m
					}
				);
			});
		}
	}
}
