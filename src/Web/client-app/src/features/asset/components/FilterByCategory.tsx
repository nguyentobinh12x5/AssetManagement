import React, { useEffect } from "react";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import { useAppDispatch, useAppState } from "../../../redux/redux-hooks";
import { getAssetCategories, setAssetQuery } from "../reducers/asset-slice";
import useAssetList from "../list/useAssetList";

interface FilterByCategoryProps {}

const FilterByCategory: React.FC<FilterByCategoryProps> = () => {
  const {
    categories,
    assetQuery: { category },
  } = useAppState((state) => state.assets);
  const { handleFilterByCategory } = useAssetList();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (categories.length <= 1) dispatch(getAssetCategories());
  }, [categories, dispatch]);

  // Define the type for asset categories
  type AssetCategory = (typeof categories)[number];

  // Mapping of actual values to display values
  const assetCategoriesMap: Record<AssetCategory, string> = categories.reduce(
    (acc, category) => {
      acc[category as AssetCategory] = category;
      return acc;
    },
    {} as Record<AssetCategory, string>
  );

  // Get the list of display values from the map
  const displayAssetCategories = Object.values(assetCategoriesMap);

  const handleCategoryChange = (displayCategories: string[]) => {
    if (displayCategories.length === 0) {
      handleFilterByCategory(["All"]);
    } else {
      // Convert display categories to actual categories
      const actualCategories: AssetCategory[] = displayCategories
        .map(
          (displayCategory) =>
            (Object.keys(assetCategoriesMap) as AssetCategory[]).find(
              (key) => assetCategoriesMap[key] === displayCategory
            )!
        )
        .filter((category) => category !== "All");

      handleFilterByCategory(actualCategories);
    }
  };

  return (
    <DropdownFilter
      placeholder="Category"
      label="Category"
      options={displayAssetCategories}
      selectedOptions={category.map(
        (category) => assetCategoriesMap[category as AssetCategory]
      )}
      handleOptionChange={handleCategoryChange}
    />
  );
};

export default FilterByCategory;
