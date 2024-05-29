using DynamicData;
using Seniors.Context;
using Seniors.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Seniors.FilterModel;

namespace Seniors.ViewModels
{
    internal class ListDishViewModel : ViewModelBase
    {
        private SourceList<Dish> _dishSourse;
        private ReadOnlyObservableCollection<Dish> _dishs;
        public ReadOnlyObservableCollection<Dish> Dishs => _dishs;
        // Поиск.
        private string _searchString = String.Empty;
        public string SearchString
        {
            get => _searchString;
            set => this.RaiseAndSetIfChanged(ref _searchString, value);
        }
        // Фильтрация.
        private ObservableCollection<FilterModel<TypeDish>> _typeDishs;
        public ObservableCollection<FilterModel<TypeDish>> TypeDishes => _typeDishs;
        // Сортировка.
        private int _sortSelectedIndex = -1;
        public int SortSelectedIndex
        {
            get => _sortSelectedIndex;
            set => this.RaiseAndSetIfChanged(ref _sortSelectedIndex, value);
        }
        public ListDishViewModel()
        {
            Title = "Блюда";
            IconViewModel = "Assets/DishIcon.png";
            IconViewModelSelected = "Assets/DishIconSelected.png";
            // Поиск.
            var searchFilter = this.WhenAnyValue(a => a.SearchString).Select(SearchFunc);
            // Фильтрация.
            _typeDishs = new ObservableCollection<FilterModel<TypeDish>>(Locator.Current.GetService<DataBaseContext>().TypeDishes
                        .Select(a => new FilterModel<TypeDish>() {IsChecked = true, Value = a})
                        .ToList());
            var typeDishFilter = _typeDishs.ToObservableChangeSet().AutoRefresh(a => a.IsChecked)
                .Filter(a => a.IsChecked).ToCollection().Select(FilterFunc);
            // Сортировка.
            var sort = this.WhenAnyValue(a => a.SortSelectedIndex).Select(index =>
            {
                switch (index)
                {
                    case 0:
                        return SortExpressionComparer<Dish>.Ascending(a => a.Name);
                    case 1:
                        return SortExpressionComparer<Dish>.Descending(a => a.Name);
                    case 2:
                        return SortExpressionComparer<Dish>.Ascending(a => a.Price);
                    case 3:
                        return SortExpressionComparer<Dish>.Descending(a => a.Price);
                    default:
                        return SortExpressionComparer<Dish>.Ascending(a => a.Id);
                }
            });
            _dishSourse = new SourceList<Dish>();
            _dishSourse.Connect().Filter(typeDishFilter).Filter(searchFilter).Sort(sort).Bind(out _dishs).Subscribe();
            IList<Dish> dishes = Locator.Current.GetService<DataBaseContext>().Dishes
                .Include(a => a.TypeDishes)
                .ToList();
            _dishSourse.AddRange(dishes);
        }
        // Поиск.
        private Func<Dish, bool> SearchFunc(string searchString) => 
            dish => dish.Name.Contains(searchString) || dish.Notes!.Contains(searchString);
        // Фильтрация.
        private Func<Dish, bool> FilterFunc(IReadOnlyCollection<FilterModel<TypeDish>> filterModels) => 
            dish => filterModels.Count == 0 || dish.TypeDishes.Any(typeDish => filterModels.Select(a => a.Value).Contains(typeDish));
    }
}