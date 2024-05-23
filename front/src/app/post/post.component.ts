import { Component, inject } from '@angular/core';
import { PostService } from './post.service';
import { Post, PostFilter } from './post';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [PostComponent, FormsModule, CommonModule],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css',
})
export class PostComponent {
  posts: Post[] = [];
  service = inject(PostService);
  filter: PostFilter = {
    tags: '',
    sortBy: 'id',
    direction: 'asc',
  };
  sortColumns = ['id', 'reads', 'likes', 'popularity'];
  sortDirections = ['asc', 'desc'];
  popularTag = ['tech', 'startup', 'culture'];
  loading = false;

  getPost() {
    if (this.filter.tags) {
      this.loading = true;
      this.service
        .getPost(this.filter)
        .then((data) => {
          this.posts = data;
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
    this.getPost();
  }

  constructor() {
    this.getPost();
  }
}
