import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { map, ReplaySubject } from "rxjs";
import { UserDTO } from "../DTOs/UserDTO";
import { ResponseResult } from '../DTOs/common/ResponseResult';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = 'https://localhost:7226/api/';
  private currentUserSource = new ReplaySubject<UserDTO>(1)
  currentUser = this.currentUserSource.asObservable();

  constructor(private httpClient: HttpClient) {
  }

  login(model: any) {
    return this.httpClient.post(`${this.baseUrl}account/login`, model).pipe(map((response) => {

      var responseResult = <ResponseResult>response;
      const user: UserDTO = <UserDTO>responseResult.data;
      
      if (user) {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
      }

    }))
  }

  register(model: any) {
    return this.httpClient.post(this.baseUrl + 'account/register', model).pipe(
      map((result: any) => {

        console.log(result);

        if (result.isSuccess && result.data != undefined) {
          localStorage.setItem('user', JSON.stringify(result.data));
          this.currentUserSource.next(result.data);
        }

        return result;
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: UserDTO) {
    if (user != null && user.token !== null && user.token !== undefined && user.token !== '')
      this.currentUserSource.next(user);
  }

}
