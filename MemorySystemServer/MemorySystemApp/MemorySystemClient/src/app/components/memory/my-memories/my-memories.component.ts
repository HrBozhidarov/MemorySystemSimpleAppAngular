import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { MemoryService } from 'src/app/services/memory/memory.service';
import { LocalStorageService } from 'src/app/share/services/local-storage.service';
import { ACCOUNT_KEYS } from '../../../constants/constants';

@Component({
  selector: 'app-my-memories',
  templateUrl: './my-memories.component.html',
  styleUrls: ['./my-memories.component.css'],
})
export class MyMemoriesComponent implements OnInit {
  public memories: any[] = [];
  public page: number = 1;

  constructor(
    private memoryService: MemoryService,
    private localStorageService: LocalStorageService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    let key = this.localStorageService.getItem(ACCOUNT_KEYS.MEMORY_CATEGORY);
    if (!key) {
      key = 'All';

      this.localStorageService.setItem(ACCOUNT_KEYS.MEMORY_CATEGORY, key);
    }

    this.memoryService.userMemories(key).subscribe(data => this.memories = data.data);
  }

  public setLike(memory: any) {
    if (!memory || memory.IsLikedFromCurrentUser) {
      return;
    }

    const likes = memory.likes;

    this.memoryService.likeMemory(memory.id).subscribe((data: any) => {
      memory.likes = data.data;
      if (memory.likes > likes) {
        memory.isLikedFromCurrentUser = true;
      } else {
        memory.isLikedFromCurrentUser = false;
      }
    },
      error => {
        this.toastrService.error(error?.error?.errorMessage);
      });
  }

  public getMemoryByCategory(category: string) {
    this.memoryService.userMemories(category).subscribe(data => this.memories = data.data);
  }
}
