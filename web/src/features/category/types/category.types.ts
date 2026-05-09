export interface Category {
    id: string;
    categoryName: string;
    description?: string;
}

export interface CreateCategoryRequest {
    categoryName: string;
    description?: string;
}

export interface UpdateCategoryRequest {
    id: string;
    categoryName: string;
    description?: string;
}