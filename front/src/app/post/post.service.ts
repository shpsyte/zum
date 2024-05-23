import { Injectable } from '@angular/core';
import { Post, PostFilter } from './post';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  getPost(filter: PostFilter): Promise<Post[]> {
    let { tags, sortBy, direction } = filter;
    tags = encodeURIComponent(tags);
    sortBy = encodeURIComponent(sortBy) || 'id';
    direction = encodeURIComponent(direction) || 'desc';

    const url = `http://localhost:5194/posts?tags=${tags}&sortBy=${sortBy}&direction=${direction}`;

    return new Promise((resolve) => {
      fetch(url)
        .then((res) => res.json())
        .then((data) => {
          resolve(data);
        });
    });
  }

  constructor() { }
}
