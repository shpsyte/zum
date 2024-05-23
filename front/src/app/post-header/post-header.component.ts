import { Component, EventEmitter, Input, Output } from "@angular/core";
import { PostFilter } from "../post/post";
import { FormsModule } from "@angular/forms";

@Component({
  selector: "app-post-header",
  standalone: true,
  imports: [FormsModule],
  templateUrl: "./post-header.component.html",
  styleUrl: "./post-header.component.css",
})
export class PostHeaderComponent {
  sortColumns = ["id", "reads", "likes", "popularity"];
  sortDirections = ["asc", "desc"];

  @Input({ required: true })
  filter!: PostFilter;

  @Output()
  filterChange = new EventEmitter<PostFilter>();

  handleFilterChange() {
    this.filterChange.emit(this.filter);
  }
}
