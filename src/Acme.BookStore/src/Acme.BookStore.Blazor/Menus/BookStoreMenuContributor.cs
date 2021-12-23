﻿using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.Permissions;
using Volo.Abp.UI.Navigation;

namespace Acme.BookStore.Blazor.Menus;

public class BookStoreMenuContributor : IMenuContributor
{
	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.Main)
		{
			await ConfigureMainMenuAsync(context);
		}
	}

	private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
	{
		var l = context.GetLocalizer<BookStoreResource>();

		context.Menu.Items.Insert(
			0,
			new ApplicationMenuItem(
				BookStoreMenus.Home,
				l["Menu:Home"],
				"/",
				"fas fa-home"
			)
		);

		var bookStoreMenu = new ApplicationMenuItem(
			"BooksStore",
			l["Menu:BookStore"],
			icon: "fa fa-book"
		);

		context.Menu.AddItem(bookStoreMenu);

		if (await context.IsGrantedAsync(BookStorePermissions.Books.Default))
		{
			bookStoreMenu.AddItem(new ApplicationMenuItem(
				"BooksStore.Books",
				l["Menu:Books"],
				"/books"
			));
		}

		if (await context.IsGrantedAsync(BookStorePermissions.Authors.Default))
		{
			bookStoreMenu.AddItem(new ApplicationMenuItem
			(
				"BooksStore.Authors",
				l["Menu:Authors"],
				"/authors"
			));
		}
	}
}