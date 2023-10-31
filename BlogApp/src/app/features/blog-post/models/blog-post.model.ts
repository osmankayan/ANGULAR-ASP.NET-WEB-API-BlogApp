import { listcategory } from "../../category/models/list-category.model";

export interface BlogPost {
    id: string,
    title: string;
    shortDescription: string;
    content: string;
    featuredImageUrl: string;
    urlHandle: string;
    author: string;
    publishedDate: Date;
    isVisible: boolean;
    categories:listcategory[];
}