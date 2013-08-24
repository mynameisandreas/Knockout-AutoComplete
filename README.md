Knockout-AutoComplete
=====================

Knockout binding for autocomplete.

How to use the Knockout Autocompletet binding in 6 simple ways.

1. Get the binding from nuget and install it: 
	Install-package KnockoutAutocomplete

2. Reference the ko.autocomplete.js file to your site and css: 
	<link href="~/Content/ko.binding.style.css" rel="stylesheet" />
	<script src="~/Scripts/ko.autocomplete.js"></script>

3. Add a element that uses the binding: 
	(Configuration)
		A url that should return a list of json.
		Items should contain a "id" property and a "label" property
		A selectCallback function that will be executed when clicking (selecting a item)

		<input type="text" data-bind="autoComplete: search, settings: { url : '/My/Service/ReturnJson', selectCallback : selectItemCallback}"/>

4. Add a observable property for your viewmodel
	self.search = ko.observable('').extend({ throttle: 300 });

	Adding a throttle so we dont make a request to the server each keypress

5. Add a function to your viewmodel that will be executed when clicking ( selecting an item)
	self.selectItemCallback = function(data)
	{
		//dosomething with the selected item
		console.log(data);
	}

6. Enyoj!
