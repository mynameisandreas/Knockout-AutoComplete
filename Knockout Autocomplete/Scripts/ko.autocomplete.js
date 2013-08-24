/*
About the author: http://www.andreasgustafsson.se
*/

var viewModel = new SearchResultViewModel();
var focusedItem = -1;

ko.bindingHandlers.autoComplete = {
    init: function (element, valueAccessor, allBindingsAccessor) {

        //We want valueupdate on keydown
        allBindingsAccessor().valueUpdate = 'afterkeydown';
        //get the settings from the html control
        var settings = allBindingsAccessor().settings || {};

        //create ul that will be added to the DOM
        var ul = '<ul class="knockout-autoComplete" data-bind="foreach: searchResults"><li data-bind="text: label, click : $parent.select.bind($data), css : {\'autocomplete-selected\' : focus}"></li></ul>';

        //Add ul to DOM after input (searchbox)
        $(element).after(ul);

        //Add blur event for cleaning searchresults
        $(element).blur(function () {
            //Timeout function to be able to Click on a item before the lists dissapear
            setTimeout(function () {
                //clear searchresults
                viewModel.searchResults([]);
                //clear elements value 
                $(element).val('');
            }, 150);
        });

        //Key navigation
        ko.utils.registerEventHandler(element, 'keydown', function (evt) {
            var item;

            switch (evt.keyCode) {
                //Esc clicked blur element
                case 27:
                    $(element).trigger('blur');
                    break;
                    //38 Key upp
                case 38:
                    if (focusedItem >= 0) {
                        viewModel.toggleSelected(focusedItem);
                        focusedItem--;
                        viewModel.toggleSelected(focusedItem);
                    }
                    break;
                    //40 keydown
                case 40:
                    if (focusedItem < viewModel.searchResults().length) {
                        viewModel.toggleSelected(focusedItem);
                        focusedItem++;
                        viewModel.toggleSelected(focusedItem);
                    }
                    break;
                case 13:
                    //Enter select item with index of focusedItem
                    item = viewModel.searchResults()[focusedItem];
                    viewModel.select(item);
                    break;
            }

        });

        //Set element in viewModel
        viewModel.setElement(element);
        //set selectFunction of item
        viewModel.setSelectFunction(settings.selectCallback);

        //Bind ul with viewModel
        ko.applyBindings(viewModel, $(element).next('ul')[0]);
        ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor();
        var settings = allBindingsAccessor().settings || {};

        //Remove old results
        viewModel.searchResults.removeAll();

        //Only do search if input value is longer then 0
        if (value().length > 0) {

            //Ajax call to server
            $.ajax({
                url: settings.url,
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                data: { searchstring: value },
                success: function (data) {
                    //parse result 
                    data = JSON.parse(data);
                    //Map data that is returned from server
                    var mapped = ko.utils.arrayMap(data, function (item) {
                        return { label: item.label, value: item.id, focus: ko.observable(false) };
                    });
                    
                    //put the mapped data to the viewModels searchresults array
                    viewModel.searchResults(mapped);
                }
            });
        }

        ko.bindingHandlers.value.update(element, value, allBindingsAccessor);
    }
};


//Autocomplete Viewmodel
function SearchResultViewModel() {
    var self = this;
    
    //array for searchresults
    self.searchResults = ko.observableArray([]);
    //bound element (for cleaning value during select)
    self.element = null;
    //function that will be triggerd on select
    self.selectFunction = null;

    self.setSelectFunction = function (selectFunction) {
        if (selectFunction) {
            self.selectFunction = selectFunction;
        }
    };

    self.setElement = function (element) {
        if (element) {
            self.element = element;
        }
    };

    //When navigating up and down toggle what item is selected and not
    self.toggleSelected = function (index) {
        var item = viewModel.searchResults()[index];
        if (item) {
            item.focus(!item.focus());
        }
    };

    //item click function
    self.select = function (data) {
        self.searchResults([]);
        $(self.element).val('');
        //trigger custom select function
        self.selectFunction(data);
    };
}
