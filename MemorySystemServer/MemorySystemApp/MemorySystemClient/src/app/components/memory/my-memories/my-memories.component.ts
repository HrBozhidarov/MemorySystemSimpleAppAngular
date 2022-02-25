import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { MemoryService } from 'src/app/services/memory/memory.service';
import { LocalStorageService } from 'src/app/share/services/local-storage.service';

@Component({
  selector: 'app-my-memories',
  templateUrl: './my-memories.component.html',
  styleUrls: ['./my-memories.component.css'],
})
export class MyMemoriesComponent implements OnInit {
  public pictures: any[] = [];
  public page: number = 1;

  constructor(
    private memoryService: MemoryService,
    private localStorageService: LocalStorageService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    let key = this.localStorageService.getItem('my-memory-category-key');
    if (!key) {
      key = 'All';

      this.localStorageService.setItem('my-memory-category-key', key);
    }

    this.memoryService.myMemories(key).subscribe(data => this.pictures = data.data);
  }

  public setLike(picture: any) {
    if (!picture || picture.IsLikedFromCurrentUser) {
      return;
    }

    const likes = picture.likes;

    this.memoryService.likePicture(picture.id).subscribe((data: any) => {
      picture.likes = data.data;
      if (picture.likes > likes) {
        picture.isLikedFromCurrentUser = true;
      } else {
        picture.isLikedFromCurrentUser = false;
      }
    },
      error => {
        this.toastrService.error(error?.error?.errorMessage);
      });
  }

  public getPictureByCategory(category: string) {
    this.memoryService.myMemories(category).subscribe(data => this.pictures = data.data);
  }
}
