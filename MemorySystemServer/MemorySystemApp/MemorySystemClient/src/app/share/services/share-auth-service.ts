import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

@Injectable()
export class ShareAuthService {
    private dataSource = new BehaviorSubject<boolean>(localStorage.getItem("token") != null);

    data = this.dataSource.asObservable();

    updatedDataSelection(data: boolean) {
        this.dataSource.next(data);
    }
}
