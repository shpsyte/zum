import { Component, inject } from '@angular/core';
import { PostService } from './services/post.service';
import { Post } from './types/post';
import { PostFilter } from './types/PostFilter';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PostHeaderComponent } from './components/header/post-header.component';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [PostComponent, FormsModule, CommonModule, PostHeaderComponent],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css',
})
export class PostComponent {
  constructor() {
    this.getPost(this.filter);
  }

  posts: Post[] = [];
  service = inject(PostService);
  filter: PostFilter = {
    tags: '',
    sortBy: 'id',
    direction: 'asc',
  };
  error = '';
  loading = false;

  getPost(filter: PostFilter) {
    if (this.filter.tags) {
      this.loading = true;
      this.error = '';
      this.service
        .getPost(filter)
        .then((data) => {
          this.posts = data;
        })
        .catch((e) => {
          this.loading = false;
          this.posts = [];
          this.error = e;
        })
        .finally(() => {
          this.loading = false;
        });
    } else {
      this.posts = [];
    }
  }

  sortBy(column: string) {
    this.filter.sortBy = column;
    this.filter.direction = this.filter.direction === 'asc' ? 'desc' : 'asc';
    const filter = { ...this.filter };
    this.getPost(filter);
  }

  handleFilterChange(filter: PostFilter) {
    this.error = '';
    this.filter = filter;
    this.getPost(filter);
  }
}
