import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { UserDTO } from 'src/app/DTOs/UserDTO';
import { MemberDTO } from 'src/app/DTOs/member/MemberDTO';
import { AccountService } from 'src/app/services/account.service';
import { MemberService } from 'src/app/services/member.service';

@Component({
  selector: 'app-edit-member',
  templateUrl: './edit-member.component.html',
  styleUrls: ['./edit-member.component.css']
})
export class EditMemberComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm;
  member: MemberDTO;
  user: UserDTO;

  constructor(private accountService: AccountService, private memberService: MemberService, private toasterService: ToastrService) {
    this.accountService.currentUser.pipe(take(1)).subscribe(user => {
      this.user = user;
    });
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    console.log('user is : ', this.user)
    this.memberService.getMember(this.user.userName).subscribe(member => {
      this.member = member;
    });
  }

  updateMember() {
    this.memberService.updateMember(this.member).subscribe(() => {
      this.toasterService.success('حساب کاربری شما با موفقیت ویرایش شد.');
      this.editForm.reset(this.member);
    });
  }

}
